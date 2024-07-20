using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MemberWebApplication.Controllers
{
    public class CommonController : Controller
    {
        
        MemberManagementSystemDBEntities db = new MemberManagementSystemDBEntities();

        // GET: Common
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
                    Session["User"] = new Users
                    {
                        U_ID = ModelUser.U_ID,
                        U_Role = ModelUser.U_Role,
                        U_RealName = ModelUser.U_RealName,
                        S_ID = ModelUser.S_ID,
                    };
                    System.Web.Security.FormsAuthentication.SetAuthCookie(ModelUser.U_Role.ToString(), false);
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
            if (Session["User"] == null)
            {
                return Json(new
                {
                    Login = false
                },JsonRequestBehavior.AllowGet);
            }
            Users user = (Users)Session["User"];
            return Json(new
            {
                U_ID = user.U_ID,
                U_Role = user.U_Role,
                U_RealName = user.U_RealName,
                S_ID = user.S_ID,
            }, JsonRequestBehavior.AllowGet);
        }

    }
}