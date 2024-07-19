using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MemberWebApplication.Controllers
{
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

        public ActionResult GetShopsInfo(int limit, int offset)
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
            //var shops = db.Shops
            //    .OrderBy(s => s.S_ID)
            //    .Skip(offset)
            //    .Take(limit)
            //    .ToList();
            var result = new
            {
                rows = shops,
                total = totalCount
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }

}
