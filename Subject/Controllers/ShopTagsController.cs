using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subject.Models;

namespace Subject.Controllers
{
    public class ShopTagsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();

        // GET: ShopTags
        public ActionResult Index()
        {
            var shopTag = db.ShopTag.Include(s => s.Shop).Include(s => s.Users);
            return View(shopTag.ToList());
        }

        // GET: ShopTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTag shopTag = db.ShopTag.Find(id);
            if (shopTag == null)
            {
                return HttpNotFound();
            }
            return View(shopTag);
        }

        // GET: ShopTags/Create
        public ActionResult Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount");
            return View();
        }

        // POST: ShopTags/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagNumber,UserNumber,ShopNumber,Tag,TagDate")] ShopTag shopTag)
        {
            if (ModelState.IsValid)
            {
                db.ShopTag.Add(shopTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopTag.UserNumber);
            return View(shopTag);
        }

        // GET: ShopTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTag shopTag = db.ShopTag.Find(id);
            if (shopTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopTag.UserNumber);
            return View(shopTag);
        }

        // POST: ShopTags/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagNumber,UserNumber,ShopNumber,Tag,TagDate")] ShopTag shopTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopTag.UserNumber);
            return View(shopTag);
        }

        // GET: ShopTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopTag shopTag = db.ShopTag.Find(id);
            if (shopTag == null)
            {
                return HttpNotFound();
            }
            return View(shopTag);
        }

        // POST: ShopTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopTag shopTag = db.ShopTag.Find(id);
            db.ShopTag.Remove(shopTag);
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
