using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using Subject.Models;

namespace Subject.Controllers
{
    public class ShopImagesController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

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
        public ActionResult _Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount");
            return PartialView();
        }

        //POST: ShopImages/Create
        //若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ImageNumber,UserNumber,ShopNumber,ShopImage1,ImageDate")] ShopImage shopImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ShopImage.Add(shopImage);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
        //    ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
        //    return View(shopImage);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageNumber,UserNumber,ShopNumber,ShopImage1,ImageDate")] ShopImage shopImage, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                int filelength = upload.ContentLength;
                byte[] Myfile = new byte[filelength];
                upload.InputStream.Read(Myfile, 0, filelength);
                shopImage.ShopImage1 = Myfile;

                shopImage.ImageDate=DateTime.Now;

                db.ShopImage.Add(shopImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
            return View(shopImage);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ShopImage shopImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string sql = "insert into ShopImage(UserNumber,ShopNumber,ShopImage)" +
        //        "values(@UserNumber,@ShopNumber,CONVERT(VarBinary(MAX),'@ShopImage'))";

        //        List<SqlParameter> list = new List<SqlParameter>
        //        {
        //            new SqlParameter("UserNumber",shopImage.UserNumber),
        //            new SqlParameter("ShopNumber",shopImage.ShopNumber),
        //            new SqlParameter("ShopImage",shopImage.ShopImage1)
        //        };
        //        sd.executeSql(sql, list);
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
        //    ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserAccount", shopImage.UserNumber);
        //    return View(shopImage);
        //}



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

        //public FileResult GetPhoto(int? id)
        //{
            
        //    ShopImage shopImage = db.ShopImage.Find(id);

        //    byte[] photo = shopImage.ShopImage1;

        //    return File(photo, "image/jpeg");

        //}

        public FileContentResult GetPhoto(int id)
        {
            var photo = db.ShopImage.Find(id);
            if (photo != null)
                return File(photo.ShopImage1,"image/jpeg");
            return null;

        }




    }
}
