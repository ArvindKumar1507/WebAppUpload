using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.DataAccess;
using SampleWebApp.Models;
using System.Linq;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppUploadContext _context;
        public HomeController(WebAppUploadContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadFiles([FromForm] IFormCollection formData)
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
            if (IsFileIDAlreadyInUse(fileId)) {            
                return Json(new { Status = false, Msg = "FileId already used" });
            }
            AddFile(fileId, file);
            using (var stream = System.IO.File.Create(Path.Combine("D:\\Upload", file.FileName)))
            {
                await file.CopyToAsync(stream);              
            }
            return Json(new { Status = true, Msg = "File Uploaded Successfully" });
        }

        private void AddFile(string fileId, IFormFile file)
        {
            FileDetails fileDetails = new FileDetails()
            {
                FileID = fileId,
                FileName = file.FileName,
                FilePath = Path.Combine("D:\\Upload", file.FileName),
                FileBytes = file.Length.ToString(),
                FileType = Path.GetExtension(file.FileName)
            };
            _context.FileDetails.Add(fileDetails);
            _context.SaveChanges();
        }

        private bool IsFileIDAlreadyInUse(string fileId)
        {
            return !string.IsNullOrWhiteSpace(fileId) && _context.FileDetails.Any(wh => wh.FileID == fileId);        
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
