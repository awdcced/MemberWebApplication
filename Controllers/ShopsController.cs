using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberWebApplication.Models;

namespace MemberWebApplication.Controllers
{
    public class ShopsController : Controller
    {
        private MemberManagementSystemDBEntities db = new MemberManagementSystemDBEntities();

        // GET: Shops
        public ActionResult Index()
        {
            return View(db.Shops.ToList());
        }

        // GET: Shops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shops shops = db.Shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // GET: Shops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_ID,S_Name,S_Category,S_ContactName,S_ContactTel,S_Address,S_Remark,S_IsHasSetAdmin,S_CreateTime")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                db.Shops.Add(shops);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shops);
        }

        // GET: Shops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shops shops = db.Shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // POST: Shops/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_ID,S_Name,S_Category,S_ContactName,S_ContactTel,S_Address,S_Remark,S_IsHasSetAdmin,S_CreateTime")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shops).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shops);
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shops shops = db.Shops.Find(id);
            if (shops == null)
            {
                return HttpNotFound();
            }
            return View(shops);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shops shops = db.Shops.Find(id);
            db.Shops.Remove(shops);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
