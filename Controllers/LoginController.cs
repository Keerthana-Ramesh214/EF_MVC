using EntityFramewor_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramewor_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly Bank_DbContext _context;

        public LoginController(Bank_DbContext context)
        {
            _context = context;
        }

        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginUser(LoginCredential l)
        {
            string UserName = l.UserName;
            HttpContext.Session.SetString("Username", UserName);
            string Password = l.PassWord;
            var result = (from i in _context.LoginCredentials where i.UserName == l.UserName && i.PassWord == l.PassWord select i).FirstOrDefault();
            if (result != null)
            {
                return RedirectToAction("Index", "Accounts");
            }
            else
            {
                return View();
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginUser");
        }
    }
}
