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
                return Json(new GenericResponse { Message = "User Name exists", Status = false });
            }
            if (userDetails != null)
            {
                _context.UserDetails.Add(userDetails);
                _context.SaveChanges();
                return Json(new GenericResponse { Message = "Signed Up successfully", Status = true });
            }
            return Json(new GenericResponse { Message = "Please try again later", Status = false });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SingIn([FromBody] UserDetails userDetails)
        {
            var signInUser = _context.UserDetails.FirstOrDefault(wh => wh.Email == userDetails.Email && wh.Password == userDetails.Password);
            if (signInUser == null) 
            {
                return Json(new GenericResponse { Message = "Invalid Email or Password", Status = false });
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInUser.UserName),
                new Claim(ClaimTypes.Email, signInUser.Email),                
                new Claim(ClaimTypes.UserData, signInUser.ToString()),
            };

            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(new GenericResponse { Message = "Singed in Successfully", Status = true });
        }

               
        public void SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
