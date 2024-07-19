using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName,string PassWord)
        {
            if (ModelState.IsValid)
            {
                var ModelUser = db.Users.FirstOrDefault(u => u.U_LoginName == UserName && u.U_Password == PassWord);
                if (ModelUser != null)
                {
                    var dtoModelUser = new Users
                    {
                        U_ID = ModelUser.U_ID,
                    };
                }
            }
            return View();
        }
    }
}