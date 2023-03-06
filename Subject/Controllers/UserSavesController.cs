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
    public class UserSavesController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();

        // GET: UserSaves
        public ActionResult Index()
        {
            var userSave = db.UserSave.Include(u => u.Shop).Include(u => u.Users);
            return View(userSave.ToList());
        }

        // GET: UserSaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSave userSave = db.UserSave.Find(id);
            if (userSave == null)
            {
                return HttpNotFound();
            }
            return View(userSave);
        }

        // GET: UserSaves/Create
        public ActionResult Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount");
            return View();
        }

        // POST: UserSaves/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number,UserNumber,ShopNumber,SaveDate")] UserSave userSave)
        {
            if (ModelState.IsValid)
            {
                db.UserSave.Add(userSave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", userSave.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", userSave.UserNumber);
            return View(userSave);
        }

        // GET: UserSaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSave userSave = db.UserSave.Find(id);
            if (userSave == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", userSave.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", userSave.UserNumber);
            return View(userSave);
        }

        // POST: UserSaves/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number,UserNumber,ShopNumber,SaveDate")] UserSave userSave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", userSave.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", userSave.UserNumber);
            return View(userSave);
        }

        // GET: UserSaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSave userSave = db.UserSave.Find(id);
            if (userSave == null)
            {
                return HttpNotFound();
            }
            return View(userSave);
        }

        // POST: UserSaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSave userSave = db.UserSave.Find(id);
            db.UserSave.Remove(userSave);
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
