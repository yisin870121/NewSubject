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
   
    public class PostShopsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: PostShops
        public ActionResult Index()
        {
            var postShop = db.PostShop.Include(p => p.Adm).Include(p => p.Users);
            return View(postShop.ToList());
        }

        // GET: PostShops/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShop postShop = db.PostShop.Find(id);
            if (postShop == null)
            {
                return HttpNotFound();
            }
            return PartialView(postShop);
        }

        // GET: PostShops/Create
        public ActionResult _Create()
        {
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return PartialView();
        }

        // POST: PostShops/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostShop postShop)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into PostShop(PostName,PostDistrict,PostAdress,PostTime,PostPhone,PostWeb,PostOutlet," +
                    "PostWifi,PostLimitedTime,PostOrder,UserNumber,AdmNumber)values(@PostName,@PostDistrict,@PostAdress," +
                    "@PostTime,@PostPhone,@PostWeb,@PostOutlet,@PostWifi,@PostLimitedTime,@PostOrder,@UserNumber,@AdmNumber)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("PostName",postShop.PostName),
                    new SqlParameter("PostDistrict",postShop.PostDistrict),
                    new SqlParameter("PostAdress",postShop.PostAdress),
                    new SqlParameter("PostTime",postShop.PostTime),
                    new SqlParameter("PostPhone",postShop.PostPhone),
                    new SqlParameter("PostWeb",postShop.PostWeb),
                    new SqlParameter("PostOutlet",postShop.PostOutlet),
                    new SqlParameter("PostWifi",postShop.PostWifi),
                    new SqlParameter("PostLimitedTime",postShop.PostLimitedTime),
                    new SqlParameter("PostOrder",postShop.PostOrder),
                    new SqlParameter("UserNumber",postShop.UserNumber),
                    new SqlParameter("AdmNumber",postShop.AdmNumber)
                };

                sd.executeSql(sql,list);
                return RedirectToAction("Index");
               
            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", postShop.AdmNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", postShop.UserNumber);
            return View(postShop);
        }

        // GET: PostShops/Edit/5
        
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShop postShop = db.PostShop.Find(id);
            if (postShop == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", postShop.AdmNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", postShop.UserNumber);
            return PartialView(postShop);
        }

        // POST: PostShops/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostShop postShop)
        {
            if (ModelState.IsValid)
            {
                int id = ((Adm)Session["adm"]).AdmNumber;
                var adms = db.Adm.Find(id);

                string sql = "update PostShop set PassDate=@PassDate,AdmNumber=@AdmNumber " +
                    "where PostNumber=@PostNumber";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("PassDate",postShop.PassDate),
                    new SqlParameter("AdmNumber",adms.AdmNumber),
                    new SqlParameter("PostNumber",postShop.PostNumber)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }
            
                ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", postShop.AdmNumber);
                ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", postShop.UserNumber);
                return View(postShop);
            
            
        }

        // GET: PostShops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostShop postShop = db.PostShop.Find(id);
            if (postShop == null)
            {
                return HttpNotFound();
            }
            return View(postShop);
        }

        // POST: PostShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostShop postShop = db.PostShop.Find(id);
            db.PostShop.Remove(postShop);
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
