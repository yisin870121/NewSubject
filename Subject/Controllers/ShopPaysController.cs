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
    public class ShopPaysController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: ShopPays
        public ActionResult Index()
        {
            var shopPay = db.ShopPay.Include(s => s.Pay).Include(s => s.Shop);
            return View(shopPay.ToList());
        }

        // GET: ShopPays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopPay shopPay = db.ShopPay.Find(id);
            if (shopPay == null)
            {
                return HttpNotFound();
            }
            return View(shopPay);
        }

        // GET: ShopPays/Create
        public ActionResult _Create()
        {
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType");
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            return PartialView();
        }

        // POST: ShopPays/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopPay shopPay)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into ShopPay(ShopNumber,PayNumber)values(@ShopNumber,@PayNumber)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("ShopNumber",shopPay.ShopNumber),
                     new SqlParameter("PayNumber",shopPay.PayNumber)
                };

                sd.executeSql(sql,list);
                return RedirectToAction("Index");

            }

            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", shopPay.PayNumber);
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopPay.ShopNumber);
            return View(shopPay);
        }

        // GET: ShopPays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopPay shopPay = db.ShopPay.Find(id);
            if (shopPay == null)
            {
                return HttpNotFound();
            }
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", shopPay.PayNumber);
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopPay.ShopNumber);
            return View(shopPay);
        }

        // POST: ShopPays/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShopNumber,PayNumber,CreateDate")] ShopPay shopPay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopPay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", shopPay.PayNumber);
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopPay.ShopNumber);
            return View(shopPay);
        }

        // GET: ShopPays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopPay shopPay = db.ShopPay.Find(id);
            if (shopPay == null)
            {
                return HttpNotFound();
            }
            return View(shopPay);
        }

        // POST: ShopPays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopPay shopPay = db.ShopPay.Find(id);
            db.ShopPay.Remove(shopPay);
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
