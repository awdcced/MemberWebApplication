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
            var shops = db.Shops
                .OrderBy(s => s.S_ID)
                .Skip(offset)
                .Take(limit)
                .ToList();
            var result = new
            {
                rows = shops,
                total = totalCount
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }

    }
