﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        public ActionResult _Details(int? id)
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
            return PartialView(shopImage);
        }

        // GET: ShopImages/Create
        public ActionResult _Create()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return PartialView();
        }

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

                shopImage.ImageDate = DateTime.Now;

                db.ShopImage.Add(shopImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
            return View(shopImage);
        }

        [LoginCheck(id = 1)]
        public ActionResult UserCreate()
        {
            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", "2");
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(ShopImage shopImage, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                int id = ((Users)Session["user"]).UserNumber;
                var users = db.Users.Find(id);

                int filelength = upload.ContentLength;
                byte[] Myfile = new byte[filelength];
                upload.InputStream.Read(Myfile, 0, filelength);
                shopImage.ShopImage1 = Myfile;

                string sql = "insert into ShopImage(UserNumber,ShopNumber,ShopImage)" +
                    "values(@UserNumber,@ShopNumber,@ShopImage)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",users.UserNumber),
                    new SqlParameter("ShopNumber",shopImage.ShopNumber),
                    new SqlParameter("ShopImage",shopImage.ShopImage1)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index", "Home", new { id = shopImage.ShopNumber });
            }

            ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
            return View(shopImage);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UserCreate([Bind(Include = "ImageNumber,UserNumber,ShopNumber,ShopImage1,ImageDate")] ShopImage shopImage, HttpPostedFileBase upload)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id = ((Users)Session["user"]).UserNumber;
        //        var users = db.Users.Find(id);
        //        shopImage.UserNumber = users.UserNumber;

        //        int filelength = upload.ContentLength;
        //        byte[] Myfile = new byte[filelength];
        //        upload.InputStream.Read(Myfile, 0, filelength);
        //        shopImage.ShopImage1 = Myfile;

        //        shopImage.ImageDate = DateTime.Now;

        //        db.ShopImage.Add(shopImage);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Home", new { id = shopImage.ShopNumber });
        //    }

        //    ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
        //    //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
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
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
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
            ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
            return View(shopImage);
        }

        // GET: ShopImages/Delete/5
        public ActionResult _Delete(int? id)
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
            return PartialView(shopImage);
        }

        // POST: ShopImages/Delete/5
        [HttpPost, ActionName("_Delete")]
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


        public FileContentResult GetPhoto(int id)
        {
            var photo = db.ShopImage.Find(id);
            if (photo != null)
                return File(photo.ShopImage1,"image/jpeg");
            return null;

        }




    }
}
