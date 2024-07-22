using MemberWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MemberWebApplication.Controllers
{
    public class CommonController : Controller
    {
        
        MemberManagementSystemDBEntities db = new MemberManagementSystemDBEntities();

        // GET: Common
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name == "2")
                {
                    return RedirectToAction("StoreManagement", "SuperAdmin");
                }
                else if (User.Identity.Name == "2")
                {
                    return RedirectToAction("StoreManagement", "SuperAdmin");
                }
                else if (User.Identity.Name == "3")
                {
                    return RedirectToAction("StoreManagement", "SuperAdmin");
                }
            }
                return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string PassWord)
        {
            if (ModelState.IsValid)
            {
                var ModelUser = db.Users.FirstOrDefault(u => u.U_LoginName == UserName && u.U_Password == PassWord);
                if (ModelUser != null)
                {
                    var Users = new Users
                    {
                        U_ID = ModelUser.U_ID,
                        U_Role = ModelUser.U_Role,
                        U_RealName = ModelUser.U_RealName,
                        S_ID = ModelUser.S_ID,
                    };
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo.Value = HttpUtility.UrlEncode(JsonConvert.SerializeObject(Users), Encoding.GetEncoding("UTF-8"));
                    System.Web.HttpContext.Current.Response.SetCookie(userInfo);
                    userInfo.Expires = DateTime.Now.AddMinutes(30);

                    System.Web.Security.FormsAuthentication.SetAuthCookie(ModelUser.U_Role.ToString(),false);
                    if (ModelUser.U_Role == 1)
                    {
                        return RedirectToAction("StoreManagement", "SuperAdmin");
                    }
                    else if (ModelUser.U_Role == 2)
                    {
                        return RedirectToAction("StoreManagement", "SuperAdmin");
                    }
                    else if (ModelUser.U_Role == 3)
                    {
                        return RedirectToAction("StoreManagement", "SuperAdmin");
                    }

                }
            }
            return View();
            
        }
        public ActionResult GetSession()
        {
            //-- open Cookit --
            Users user = GetUser();
            //-- open Cookit --
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public static Users GetUser()
        {
            HttpCookie userInfoCookie = System.Web.HttpContext.Current.Request.Cookies.Get("userInfo");
            string strUserInfo = HttpUtility.UrlDecode(userInfoCookie.Value, Encoding.GetEncoding("UTF-8"));
            Users user = JsonConvert.DeserializeObject<Users>(strUserInfo);
            return user;
        }
    }
}