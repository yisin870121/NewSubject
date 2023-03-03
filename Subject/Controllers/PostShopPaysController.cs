using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subject.Models;

namespace Subject.Controllers
{
    public class PostShopPaysController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: PostShopPays
        public ActionResult Index()
        {
            var postShopPay = db.PostShopPay.Include(p => p.Pay).Include(p => p.PostShop);
            return View(postShopPay.ToList());
        }

        // GET: PostShopPays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShopPay postShopPay = db.PostShopPay.Find(id);
            if (postShopPay == null)
            {
                return HttpNotFound();
            }
            return View(postShopPay);
        }

        // GET: PostShopPays/Create
        public ActionResult _Create()
        {
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType");
            ViewBag.PostNumber = new SelectList(db.PostShop, "PostNumber", "PostName");
            return PartialView();
        }

        // POST: PostShopPays/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostShopPay postShopPay)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into PostShopPay(PostNumber,PayNumber)values(@PostNumber,@PayNumber)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("PostNumber",postShopPay.PostNumber),
                    new SqlParameter("PayNumber",postShopPay.PayNumber)
                };

                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }

            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", postShopPay.PayNumber);
            ViewBag.PostNumber = new SelectList(db.PostShop, "PostNumber", "PostName", postShopPay.PostNumber);
            return View(postShopPay);
        }

        // GET: PostShopPays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShopPay postShopPay = db.PostShopPay.Find(id);
            if (postShopPay == null)
            {
                return HttpNotFound();
            }
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", postShopPay.PayNumber);
            ViewBag.PostNumber = new SelectList(db.PostShop, "PostNumber", "PostName", postShopPay.PostNumber);
            return View(postShopPay);
        }

        // POST: PostShopPays/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number,PostNumber,PayNumber,CreateDate")] PostShopPay postShopPay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postShopPay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", postShopPay.PayNumber);
            ViewBag.PostNumber = new SelectList(db.PostShop, "PostNumber", "PostName", postShopPay.PostNumber);
            return View(postShopPay);
        }

        // GET: PostShopPays/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShopPay postShopPay = db.PostShopPay.Find(id);
            if (postShopPay == null)
            {
                return HttpNotFound();
            }
            return PartialView(postShopPay);
        }

        // POST: PostShopPays/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostShopPay postShopPay = db.PostShopPay.Find(id);
            db.PostShopPay.Remove(postShopPay);
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
