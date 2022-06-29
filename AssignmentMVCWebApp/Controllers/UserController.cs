using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using AssignmentMVCWebApp.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNetCore.Http;
namespace AssignmentMVCWebApp.Controllers
{
    public class UserController : Controller
    {
        UserDAL u=new UserDAL();   
  
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            User us = new User();
            us.FullName = form["fullname"].ToString();
            us.EmailID = form["emailid"].ToString();
            us.Password = form["Password"].ToString();
            us.RoleId = 2;
            int res = u.Save(us);
            if (res == 1)
                return RedirectToAction("Create");
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(IFormCollection form)
        {
            User us = new User();
            us.EmailID = form["EmailId"].ToString();
            us.Password = form["Password"].ToString();
            bool res = u.Verify(us);
            if (res == true)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
                return View();
            }
        }
    }
}
