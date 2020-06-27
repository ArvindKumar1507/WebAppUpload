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
    public class HomeController : Controller
    {
        private readonly WebAppUploadContext _context;      
        public HomeController(WebAppUploadContext context)
        {
            _context = context;           
        }

        #region Public Action methods
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard() 
        {
            return View("Dashboard");
        }

        [Authorize]
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
                return Json(new GenericResponse  { Status = false, Message = "File is Empty" });
            }          
            UpdateFile(fileId, file);                
            return Json(new GenericResponse { Status = true, Message = "File Uploaded Successfully" });
        }
      
        
        [HttpGet]
        public ActionResult GetFile(string fileId) 
        {
            var file = _context.FileDetails.FirstOrDefault(fd => fd.FileId == fileId);
            if (file == null)
            {
                return Json(new GenericResponse  { Message = "FileId does not exist.", Status = false });
            }
            byte[] fileBytes = Convert.FromBase64String(file?.FileBytes);
            return File(fileBytes, "application/octet-stream", file?.FileName);
        }

        [HttpGet]
        public JsonResult GetFiles() 
        {
            int userId = 1;//Need to get Current User Id
            return Json(_context.FileDetails.Where(wh => wh.CreatedBy == userId));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Private HelperMehtods        
        private bool IsFileIDAlreadyInUse(string fileId)
        {
            return !string.IsNullOrWhiteSpace(fileId) && _context.FileDetails.Any(wh => wh.FileId == fileId);        
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
                var fileDetails = _context.FileDetails.First(wh => wh.FileId == fileId);
                fileDetails.FileName = file.FileName;
                fileDetails.FileBytes = fileString;
                fileDetails.FileType = Path.GetExtension(file.FileName);
            }
            else
            {
                FileDetails fileDetails = new FileDetails()
                {
                    FileId = fileId,
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
