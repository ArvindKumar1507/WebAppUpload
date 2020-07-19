using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.DataAccess;
using SampleWebApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.Collections.Generic;
using SampleWebApp.Models.BusinessModels;

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

        [Authorize]
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        
        public ActionResult Service() 
        {
            return View("Service");
        }

        [Authorize]
        [HttpPost]
        public JsonResult UploadFiles([FromForm] IFormCollection formData)
        {
            IFormFile file = HttpContext.Request?.Form?.Files?[0];
            string fileId = formData["passwd"];
            if (file == null || file.Length == 0)
            {
                return Json(new GenericResponse { Status = false, Message = "File is Empty" });
            }
            if (IsFileIDAlreadyInUse(fileId)) 
            {
                return Json(new GenericResponse { Status = false, Message = "File Id is already in use." });
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
                return Json(new GenericResponse { Message = "FileId does not exist.", Status = false });
            }
            byte[] fileBytes = Convert.FromBase64String(file?.FileBytes);
            return File(fileBytes, "application/octet-stream", file?.FileName);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetFiles()
        {
          List<FileDetails> fileDetails = _context.FileDetails.Where(wh => wh.CreatedBy == CurrentUser.UserId).ToList();
            List<BusinessFile> busineesFiles = new List<BusinessFile>();
            fileDetails.ForEach(f =>
            {
                var businessFile = new BusinessFile();
                businessFile.FileId = f.FileId;
                businessFile.FileName = f.FileName;
                businessFile.FileType = f.FileType;
                businessFile.CreatedTime = f.CreatedTime;                
            });
            return Json(fileDetails);
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
            FileDetails fileDetails = new FileDetails()
            {
                FileId = fileId,
                FileName = file.FileName,
                FileBytes = fileString,
                CreatedBy = CurrentUser.UserId,
                CreatedTime = DateTime.UtcNow,
                FileType = Path.GetExtension(file.FileName)
            };
            _context.FileDetails.Add(fileDetails);
            _context.SaveChanges();
        }
        #endregion

        /// <summary>
        /// Logged in User Details
        /// </summary>
        public UserDetails CurrentUser 
        {
            get 
            {
                UserDetails userDetails = new UserDetails();
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                if (claimsIdentity != null) 
                {
                    var userData = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;
                    if(!string.IsNullOrWhiteSpace(userData))
                         userDetails = JsonSerializer.Deserialize<UserDetails>(userData);
                }
                return userDetails;
            }
        }
    }
}
