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
    public class ShopTagsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

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
        public ActionResult _Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return PartialView();
        }

        // POST: ShopTags/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopTag shopTag)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into ShopTag(UserNumber,ShopNumber,Tag)values(@UserNumber,@ShopNumber,@Tag)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",shopTag.UserNumber),
                    new SqlParameter("ShopNumber", shopTag.ShopNumber),
                    new SqlParameter("Tag", shopTag.Tag)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopTag.UserNumber);
            return View(shopTag);
        }


        // GET: ShopTags/Edit/5
        public ActionResult _Edit(int? id)
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
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopTag.UserNumber);
            return PartialView(shopTag);
        }

        // POST: ShopTags/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopTag shopTag)
        {
            string sql = "update ShopTag set UserNumber=@UserNumber,ShopNumber=@ShopNumber,Tag=@Tag from ShopTag " +
                "inner join Shop on ShopTag.ShopNumber=Shop.ShopNumber " +
                "inner join Users on ShopTag.UserNumber=Users.UserNumber " +
                "where TagNumber=@TagNumber";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("UserNumber",shopTag.UserNumber),
                new SqlParameter("ShopNumber",shopTag.ShopNumber),
                new SqlParameter("Tag",shopTag.Tag),
                new SqlParameter("TagNumber",shopTag.TagNumber)
            };

            try
            {
                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg=ex.Message;
                ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
                ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopTag.UserNumber);
                return View(shopTag);
            }

        }

        // GET: ShopTags/Delete/5
        public ActionResult _Delete(int? id)
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
            return PartialView(shopTag);
        }

        // POST: ShopTags/Delete/5
        [HttpPost, ActionName("_Delete")]
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
