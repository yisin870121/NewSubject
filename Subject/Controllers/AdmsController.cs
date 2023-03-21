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
using System.Configuration;
using Subject.ViewModels;

namespace Subject.Controllers
{
    [LoginCheck]
    public class AdmsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

       
        public ActionResult Index()
        {
            return View(db.Adm.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adm adm = db.Adm.Find(id);
            if (adm == null)
            {
                return HttpNotFound();
            }
            return View(adm);
        }

       
        public ActionResult _Create()
        {
            return PartialView();
        }

        // POST: Adms/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adm adm)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Adm(AdmAccount,AdmPsaaword)" +
                    "values(@AdmAccount,@AdmPsaaword)";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("AdmAccount",adm.AdmAccount),
                    new SqlParameter("AdmPsaaword",adm.AdmPsaaword)
                };

                sd.executeSql(sql, list);

                return RedirectToAction("Index");
            }

            return View(adm);
        }

    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adm adm = db.Adm.Find(id);
            if (adm == null)
            {
                return HttpNotFound();
            }
            return View(adm);
        }

        // POST: Adms/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdmNumber,AdmAccount,AdmPsaaword")] Adm adm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm);
        }


        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adm adm = db.Adm.Find(id);
            if (adm == null)
            {
                return HttpNotFound();
            }
            return PartialView(adm);
        }

    
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adm adm = db.Adm.Find(id);
            db.Adm.Remove(adm);
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

        [LoginCheck(flag = false)]
        public ActionResult Login()
        {
            return View();
        }

        [LoginCheck(flag = false)]
        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            string password = BR.getHashPassword(vMLogin.Password);

            var adm = db.Adm.Where(m => m.AdmAccount == vMLogin.Account && m.AdmPsaaword == vMLogin.Password).FirstOrDefault();

            if (adm == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤";
                return View(vMLogin);
            }

            Session["adm"] = adm;
            return RedirectToAction("AfterLogin");
        }

  
        public ActionResult Logout()
        {
            Session["adm"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult AfterLogin()
        {
            return View();
        }



    }
}