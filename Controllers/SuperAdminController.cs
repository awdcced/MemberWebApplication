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
            // 检查参数有效性

            // 假设你有一个数据库上下文 DbContext
            var totalCount = db.Shops.Count();
            var shops = db.Shops
                .OrderBy(s => s.S_ID)
                .Skip(offset)
                .Take(limit)
                .Select(s => new ShopDto
                {
                    S_ID = s.S_ID,
                    S_Name = s.S_Name
                    // 只包含你需要的字段  
                })
                .ToList();
            var result = new
            {
                rows = shops,
                total = totalCount
                
                
            };

            return Json(result, JsonRequestBehavior.AllowGet);
            //string jsonResult = JsonConvert.SerializeObject(shops);
            //return Content(jsonResult, "application/json");

        }
        public class ShopDto
        {
            public int S_ID { get; set; }
            public string S_Name { get; set; }
            // 只包含你需要的字段，不包括任何可能导致循环引用的导航属性  
        }

    }

    }
