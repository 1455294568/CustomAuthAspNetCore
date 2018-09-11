using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAuth2.Tools;

namespace TestAuth2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Login(string json)
        {
        //    if (username != "kevin")
        //    {
        //        return "用户名不正确";
        //    }
        //    else if (passwd != "haha")
        //    {
        //        return "密码不正确";
        //    }
        //    string str = "1|" + username + "|" + passwd + "";
        //    string token = AesTool.Encrypt(str);
        //    CookieOptions options = new CookieOptions();
        //    options.Expires = DateTime.Now.AddDays(30);
        //    HttpContext.Session.SetString("Token", token);
        //    HttpContext.Response.Cookies.Append("Token", token, options);
            return "1";
        }
    }
}