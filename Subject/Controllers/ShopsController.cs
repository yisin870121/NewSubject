using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Subject.Models;

namespace Subject.Controllers
{
    public class ShopsController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: Shops
        public ActionResult Index()
        {
            var shop = db.Shop.Include(s => s.Adm);
            return View(shop.ToList());
        }

        // GET: Shops/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }

            Session["shop"] = shop;
            return PartialView(shop);
        }

        /*[ChildActionOnly]*/  //子Action //就不能在網址上打id查詢了  //只能當partialview
        public ActionResult _MenuDetail(int id)
        {
            return PartialView(db.ShopMenu.Where(m => m.ShopNumber == id).ToList());
        }

        public ActionResult _TagDetail(int id)
        {
            return PartialView(db.ShopTag.Where(m => m.ShopNumber == id).ToList());
        }


        public ActionResult _PayDetail(int id)
        {
            return PartialView(db.ShopPay.Where(m => m.ShopNumber == id).ToList());
        }

        public ActionResult _ImageDetail(int id)
        {
            return PartialView(db.ShopImage.Where(m => m.ShopNumber == id).ToList());
        }

        public FileContentResult GetPhoto(int id)
        {
            var photo = db.ShopImage.Find(id);
            if (photo != null)
                return File(photo.ShopImage1, "image/jpeg");
            return null;

        }

        // GET: Shops/Create
        public ActionResult _Create()
        {
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber");
            return PartialView();
        }

        // POST: Shops/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shop shop)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Shop(ShopName,District,ShopAddress,ShopTime,Phone,Web,Outlet,WIFI,LimitedTime,"+
                "IsOrder,AdmNumber,Closed)values(@ShopName,@District,@ShopAddress,@ShopTime,@Phone,@Web,@Outlet,@WIFI,"+
                "@LimitedTime,@IsOrder,@AdmNumber,@Closed)";

                List<SqlParameter> list = new List<SqlParameter>()
                {
                    new SqlParameter("ShopName",shop.ShopName),
                    new SqlParameter("District",shop.District),
                    new SqlParameter("ShopAddress",shop.ShopAddress),
                    new SqlParameter("ShopTime",shop.ShopTime),
                    new SqlParameter("Phone",shop.Phone),
                    new SqlParameter("Web",shop.Web),
                    new SqlParameter("Outlet",shop.Outlet),
                    new SqlParameter("WIFI",shop.WIFI),
                    new SqlParameter("LimitedTime",shop.LimitedTime),
                    new SqlParameter("IsOrder",shop.IsOrder),
                    new SqlParameter("AdmNumber",shop.AdmNumber),
                    new SqlParameter("Closed",shop.Closed)
                };

                sd.executeSql(sql,list);
                return RedirectToAction("Index");

            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", shop.AdmNumber);
            return View(shop);
        }

        // GET: Shops/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", shop.AdmNumber);
            return PartialView(shop);
        }

        // POST: Shops/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shop shop)
        {
            string sql = "update Shop set ShopName=@ShopName,District=@District,ShopAddress=@ShopAddress,"+
                "ShopTime=@ShopTime,Phone=@Phone,Web=@Web,Outlet=@Outlet,WIFI=@WIFI,LimitedTime=@LimitedTime,"+
                "IsOrder=@IsOrder,UpdateDate=@UpdateDate,Closed=@Closed,AdmNumber=@AdmNumber " +
                "from Shop inner join Adm on Shop.AdmNumber=Adm.AdmNumber where ShopNumber=@ShopNumber";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("ShopName",shop.ShopName),
                new SqlParameter("District",shop.District),
                new SqlParameter("ShopAddress",shop.ShopAddress),
                new SqlParameter("ShopTime",shop.ShopTime),
                new SqlParameter("Phone",shop.Phone),
                new SqlParameter("Web",shop.Web),
                new SqlParameter("Outlet",shop.Outlet),
                new SqlParameter("WIFI",shop.WIFI),
                new SqlParameter("LimitedTime",shop.LimitedTime),
                new SqlParameter("IsOrder",shop.IsOrder),
                new SqlParameter("UpdateDate",shop.UpdateDate),
                new SqlParameter("Closed",shop.Closed),
                new SqlParameter("AdmNumber",shop.AdmNumber),
                new SqlParameter("ShopNumber",shop.ShopNumber)
            };

            try
            { 
                sd.executeSql(sql,list);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg = ex.Message;
                ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", shop.AdmNumber);
                return View(shop);
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(shop).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmAccount", shop.AdmNumber);
            //return View(shop);
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shop shop = db.Shop.Find(id);
            db.Shop.Remove(shop);
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
