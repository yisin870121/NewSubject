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
    public class SuggestsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();
        // GET: Suggests
        public ActionResult Index()
        {
            var suggest = db.Suggest.Include(s => s.Adm).Include(s => s.Users);
            return View(suggest.ToList());
        }

        // GET: Suggests/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggest suggest = db.Suggest.Find(id);
            if (suggest == null)
            {
                return HttpNotFound();
            }
            return PartialView(suggest);
        }

        // GET: Suggests/Create
        public ActionResult _Create()
        {
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return PartialView();
        }

        // POST: Suggests/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suggest suggest)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Suggest(UserNumber,Suggest,AdmNumber)values(@UserNumber,@Suggest,@AdmNumber)";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",suggest.UserNumber),
                    new SqlParameter("Suggest",suggest.Suggest1),
                    new SqlParameter("AdmNumber",suggest.AdmNumber)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", suggest.AdmNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", suggest.UserNumber);
            return View(suggest);
        }

        // GET: Suggests/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggest suggest = db.Suggest.Find(id);
            if (suggest == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", suggest.AdmNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", suggest.UserNumber);
            return PartialView(suggest);
        }

        // POST: Suggests/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suggest suggest)
        {
            if (ModelState.IsValid)
            {
                int id = ((Adm)Session["adm"]).AdmNumber;
                var adms = db.Adm.Find(id);

                string sql = "update Suggest set CheckDate=@CheckDate,AdmNumber=@AdmNumber " +
                "where SuggestNumber=@SuggestNumber";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("Suggest",suggest.Suggest1),
                    new SqlParameter("CheckDate",suggest.CheckDate),
                    new SqlParameter("AdmNumber",adms.AdmNumber),
                    new SqlParameter("SuggestNumber",suggest.SuggestNumber),
                };
            
                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }
                ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", suggest.AdmNumber);
                ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", suggest.UserNumber);
                return View(suggest);
        }

        // GET: Suggests/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggest suggest = db.Suggest.Find(id);
            if (suggest == null)
            {
                return HttpNotFound();
            }
            return PartialView(suggest);
        }

        // POST: Suggests/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suggest suggest = db.Suggest.Find(id);
            db.Suggest.Remove(suggest);
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
