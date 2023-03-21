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
    [LoginCheck]
    public class PaysController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: Pays
        public ActionResult Index()
        {
            return View(db.Pay.ToList());
        }

        // GET: Pays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pay.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            return View(pay);
        }

        // GET: Pays/Create
        public ActionResult _Create()
        {
            return PartialView();
        }

        // POST: Pays/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pay pay)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Pay(PayType)values(@PayType)";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("PayType",pay.PayType)
                };

                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }

            return View(pay);
        }

        // GET: Pays/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pay.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            return PartialView(pay);
        }

        // POST: Pays/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pay pay)
        {
            string sql = "update Pay set PayType=@PayType where PayNumber=@PayNumber";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("PayNumber",pay.PayNumber),
                new SqlParameter("PayType",pay.PayType)
            };

            try
            {
                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg=ex.Message;
                return View(pay);
            }

            //if (ModelState.IsValid)
            //{
            //    db.Entry(pay).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(pay);
        }

        // GET: Pays/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pay.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            return PartialView(pay);
        }

        //POST: Pays/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pay pay = db.Pay.Find(id);
            db.Pay.Remove(pay);
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
