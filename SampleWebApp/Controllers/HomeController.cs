using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.DataAccess;
using SampleWebApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SampleWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly WebAppUploadContext _context;      
        public HomeController(WebAppUploadContext context)
        {
            _context = context;           
        }

        #region Public Action methods
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult UploadFiles([FromForm] IFormCollection formData)
        {
            IFormFile file = HttpContext.Request.Form.Files?[0];
            string fileId = formData["passwd"];
            if (file == null)
            {
                throw new NullReferenceException("File not uploaded properly");
            }            
            if (file.Length == 0)
            {              
                return Json(new { Status = false, Msg = "File is Empty" });
            }          
            UpdateFile(fileId, file);                
            return Json(new { Status = true, Msg = "File Uploaded Successfully" });
        }
      
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFile(string fileId) 
        {
            var file = _context.FileDetails.FirstOrDefault(fd => fd.FileID == fileId);
            if (file == null)
            {
                return Json(new { msg = "FileId does not exist." });
            }
            byte[] fileBytes = Convert.FromBase64String(file?.FileBytes);
            return File(fileBytes, "application/octet-stream", file?.FileName);
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Private HelperMehtods        
        private bool IsFileIDAlreadyInUse(string fileId)
        {
            return !string.IsNullOrWhiteSpace(fileId) && _context.FileDetails.Any(wh => wh.FileID == fileId);        
        }

        private void UpdateFile(string fileId, IFormFile file)
        {
            string fileString = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(ms);
                fileString = Convert.ToBase64String(ms.ToArray());
            }
            if (IsFileIDAlreadyInUse(fileId))
            {
                var fileDetails = _context.FileDetails.First(wh => wh.FileID == fileId);
                fileDetails.FileName = file.FileName;
                fileDetails.FileBytes = fileString;
                fileDetails.FileType = Path.GetExtension(file.FileName);
            }
            else
            {
                FileDetails fileDetails = new FileDetails()
                {
                    FileID = fileId,
                    FileName = file.FileName,
                    FileBytes = fileString,
                    FileType = Path.GetExtension(file.FileName)
                };
                _context.FileDetails.Add(fileDetails);
            }
            _context.SaveChanges();
        }
        #endregion
    }
}
