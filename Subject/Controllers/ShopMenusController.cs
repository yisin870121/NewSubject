using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Subject.Models;

namespace Subject.Controllers
{
    public class ShopMenusController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd= new SetData();

        // GET: ShopMenus
        public ActionResult Index()
        {
            var shopMenu = db.ShopMenu.Include(s => s.Shop).Include(s => s.Users);
            return View(shopMenu.ToList());
        }

        // GET: ShopMenus/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopMenu shopMenu = db.ShopMenu.Find(id);
            if (shopMenu == null)
            {
                return HttpNotFound();
            }
            return PartialView(shopMenu);
        }

        // GET: ShopMenus/Create
        public ActionResult _Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return PartialView();
        }

        // POST: ShopMenus/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopMenu shopMenu)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into ShopMenu(UserNumber,ShopNumber,Item,Price)values(@UserNumber,@ShopNumber,@Item,@Price)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",shopMenu.UserNumber),
                    new SqlParameter("ShopNumber",shopMenu.ShopNumber),
                    new SqlParameter("Item",shopMenu.Item),
                    new SqlParameter("Price",shopMenu.Price)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopMenu.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopMenu.UserNumber);
            return View(shopMenu);

        }

        // GET: ShopMenus/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopMenu shopMenu = db.ShopMenu.Find(id);
            if (shopMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopMenu.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopMenu.UserNumber);
            return PartialView(shopMenu);
        }

        // POST: ShopMenus/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopMenu shopMenu)
        {
            string sql = "update ShopMenu set UserNumber=@UserNumber,ShopNumber=@ShopNumber,Item=@Item,Price=@Price from ShopMenu " +
                "inner join Shop on ShopMenu.ShopNumber=Shop.ShopNumber " +
                "inner join Users on ShopMenu.UserNumber=Users.UserNumber " +
                "where MenuNumber=@MenuNumber";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("UserNumber",shopMenu.UserNumber),
                new SqlParameter("ShopNumber",shopMenu.ShopNumber),
                new SqlParameter("Item",shopMenu.Item),
                new SqlParameter("Price",shopMenu.Price),
                new SqlParameter("MenuNumber",shopMenu.MenuNumber)
            };

            try
            {
                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg = ex.Message;
                ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopMenu.ShopNumber);
                ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopMenu.UserNumber);
                return View(shopMenu);
            }

        }

        // GET: ShopMenus/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopMenu shopMenu = db.ShopMenu.Find(id);
            if (shopMenu == null)
            {
                return HttpNotFound();
            }
            return PartialView(shopMenu);
        }

        // POST: ShopMenus/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopMenu shopMenu = db.ShopMenu.Find(id);
            db.ShopMenu.Remove(shopMenu);
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
