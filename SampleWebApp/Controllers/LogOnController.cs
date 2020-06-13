using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.DataAccess;
using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SampleWebApp.Controllers
{
    public class LogOnController : Controller
    {
        private readonly WebAppUploadContext _context;
        public LogOnController(WebAppUploadContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SingUp(UserDetails userDetails)
        {
            bool isUserNameExist = _context.UserDetails.Any(any => any.Email == userDetails.Email);
            if (isUserNameExist)
            {
                return Json(new { msg = "User Name exists", Status = false });
            }
            if (userDetails != null)
            {
                _context.UserDetails.Add(userDetails);
                _context.SaveChanges();
                return Json(new { msg = "Signed Up successfully", Status = true });
            }
            return Json(new { msg = "Please try again later", Status = false });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SingIn([FromBody] UserDetails userDetails)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserDetails", "arvind.kumar"));
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            HttpContext.SignInAsync(principal);
            return Json(new { msg = "Singed in Successfully" });
        }
    }
}
