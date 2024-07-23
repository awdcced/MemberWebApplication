using MemberWebApplication.Models;
using Microsoft.Ajax.Utilities;
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
        public ActionResult Login(string UserName, string PassWord, string ReturnUrl)
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

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, ModelUser.U_Role.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), false, JsonConvert.SerializeObject(Users));
                    string HashTicket = FormsAuthentication.Encrypt(Ticket);
                    HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                    HttpContext.Response.Cookies.Add(UserCookie);
                    if (!string.IsNullOrEmpty(HttpContext.Request["ReturnUrl"]))
                    {
                        HttpContext.Response.Redirect(HttpContext.Request["ReturnUrl"]);
                    }
                    else
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
                }
            }
            return View();

        }
        public ActionResult GetSession()
        {
            //-- open Cookit --
            Users user = GetUser((FormsIdentity)User.Identity);
            //-- open Cookit --
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public static Users GetUser(FormsIdentity Id)
        {
            FormsAuthenticationTicket Ticket = Id.Ticket;
            Users ThisUser = JsonConvert.DeserializeObject<Users>(Ticket.UserData);
            return ThisUser;
        }
    }
}