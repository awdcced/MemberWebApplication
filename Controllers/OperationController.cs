using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberWebApplication.Controllers
{
    public class OperationController : Controller
    {
        MemberManagementSystemDBEntities db = new MemberManagementSystemDBEntities();
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GiftManagement()
        {
            return View();
        }
        public ActionResult GetGiftsInfo(int limit, int offset)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var user = (Users)Session["User"];
            var totalCount = db.ExchangGifts.Where(o => o.S_ID == user.S_ID).Count();
            var gifts = db.ExchangGifts.Where(o => o.S_ID == user.S_ID).OrderBy(o => o.EG_ID).Skip(offset).Take(limit).ToList();
            var result = new
            {
                rows = gifts,
                total = totalCount
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}