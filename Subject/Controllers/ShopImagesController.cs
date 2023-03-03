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
    public class ShopImagesController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();

        // GET: ShopImages
        public ActionResult Index()
        {
            var shopImage = db.ShopImage.Include(s => s.Shop).Include(s => s.Users);
            return View(shopImage.ToList());
        }

        // GET: ShopImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopImage shopImage = db.ShopImage.Find(id);
            if (shopImage == null)
            {
                return HttpNotFound();
            }
            return View(shopImage);
        }

        // GET: ShopImages/Create
        public ActionResult Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount");
            return View();
        }

        // POST: ShopImages/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageNumber,UserNumber,ShopNumber,ShopImage1,ImageDate")] ShopImage shopImage)
        {
            if (ModelState.IsValid)
            {
                db.ShopImage.Add(shopImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
            return View(shopImage);
        }

        // GET: ShopImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopImage shopImage = db.ShopImage.Find(id);
            if (shopImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
            return View(shopImage);
        }

        // POST: ShopImages/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageNumber,UserNumber,ShopNumber,ShopImage1,ImageDate")] ShopImage shopImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
            return View(shopImage);
        }

        // GET: ShopImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopImage shopImage = db.ShopImage.Find(id);
            if (shopImage == null)
            {
                return HttpNotFound();
            }
            return View(shopImage);
        }

        // POST: ShopImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopImage shopImage = db.ShopImage.Find(id);
            db.ShopImage.Remove(shopImage);
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
