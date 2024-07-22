using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Sockets;

namespace MemberWebApplication.Controllers
{
    [Authorize(Roles = "1,2")]
    public class SuperAdminController : Controller
    {
        MemberManagementSystemDBEntities db = new MemberManagementSystemDBEntities();
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StoreManagement()
        {
            return View();
        }
        public ActionResult MembershipLevelManagement()
        {
            return View();
        }


        public ActionResult GetShopsInfo(int limit, int offset, string SName, string SContactName, string SAddress)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var totalCount = db.Shops.Count();
            var shops = from s in db.Shops
                        join c in db.CategoryItems
                        on s.S_Category equals c.CI_ID
                        where c.C_Category == "S_Category"
                        select new
                        {
                            s.S_ID,
                            s.S_Name,
                            s.S_ContactName,
                            s.S_ContactTel,
                            s.S_Address,
                            s.S_IsHasSetAdmin,
                            s.S_CreateTime,
                            c.CI_Name,
                        };
            if (!string.IsNullOrEmpty(SName))
            {
                shops = shops.Where(s => s.S_Name.Contains(SName));
            }
            if (!string.IsNullOrEmpty(SContactName))
            {
                shops = shops.Where(s => s.S_ContactName.Contains(SContactName));
            }
            if (!string.IsNullOrEmpty(SAddress))
            {
                shops = shops.Where(s => s.S_Address.Contains(SAddress));
            }
            //var shops = db.Shops
            //    .OrderBy(s => s.S_ID)
            //    .Skip(offset)
            //    .Take(limit)
            //    .ToList();
            var result = new
            {
                rows = shops
                .OrderBy(s => s.S_ID)
                .Skip(offset)
                .Take(limit)
                .ToList(),
                total = totalCount
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMembershipLevelsInfo(int limit, int offset, string CLevelName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var totalCount = db.CardLevels.Count();
            var levels = db.CardLevels.Where(o => true);
            if (!string.IsNullOrEmpty(CLevelName))
            {
                levels = levels.Where(l => l.CL_LevelName.Contains(CLevelName));
            }
            var result = new
            {
                rows = levels.OrderBy(o => o.CL_ID).Skip(offset).Take(limit).ToList(),
                total = totalCount
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateShop(Shops model)
        {
            model.S_IsHasSetAdmin = false;
            model.S_CreateTime = DateTime.Now;
            db.Shops.Add(model);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }

}
