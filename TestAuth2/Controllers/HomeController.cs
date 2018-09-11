using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAuth2.Dbcontext;
using TestAuth2.Models;
using TestAuth2.Tools;

namespace TestAuth2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public HomeController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [Authorize]
        public string Index()
        {
            return User.Identity.Name;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public string Login(User user)
        {
            try
            {
                var u = _dbcontext.Users.First(s => s.username == user.username);
                if (u.passwd == user.passwd)
                {
                    user.Id = u.Id;
                    user.passwd = "******";
                    var token = AesTool.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(user));
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Session.SetString("Token", token);
                    HttpContext.Response.Cookies.Append("Token", token, options);
                    return "1";
                }
                else
                {
                    return "账号或者密码错误";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public string Register(User user)
        {
            user.CreationTime = DateTime.Now;
            if(!ModelState.IsValid)
            {
                return "账号或密码不符合规范!";
            }
            try
            {
                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();
                return "1";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
