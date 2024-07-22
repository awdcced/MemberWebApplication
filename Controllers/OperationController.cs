using MemberWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MemberWebApplication.Controllers
{
    [Authorize(Roles = "3")]
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
        public ActionResult GetGiftsInfo(int limit, int offset, string EGiftName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var user = CommonController.GetUser((FormsIdentity)User.Identity);
            var totalCount = db.ExchangGifts.Where(o => o.S_ID == user.S_ID).Count();
            var gifts = db.ExchangGifts.Where(o => o.S_ID == user.S_ID);
            if (!string.IsNullOrEmpty(EGiftName))
            {
                gifts = gifts.Where(g => g.EG_GiftName.Contains(EGiftName));
            }
            var result = new
            {
                rows = gifts.OrderBy(o => o.EG_ID).Skip(offset).Take(limit).ToList(),
                total = totalCount
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}