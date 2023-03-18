using Subject.Models;
using Subject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Subject.Controllers
{
    public class HomeController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd= new SetData();
        
        public ActionResult Index()
        {
            var shop = db.Shop.Where(p => p.Closed == false).ToList();
            return View(shop);
        }

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

            ViewBag.ShopNumber = id;
            Session["TitleSN"] = db.Shop.Find(id).ShopName;
            return PartialView(shop);
        }

        public ActionResult Details(int? id)
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

            ViewBag.ShopNumber = id;
            Session["TitleSN"] = db.Shop.Find(id).ShopName;
            return View(shop);
        }

        public ActionResult _MenuDetail(int id)
        {
            return PartialView(db.ShopMenu.Where(m => m.ShopNumber == id).ToList());
        }

        [LoginCheck(id = 1)]
        public ActionResult UserCreateMenu()
        {
            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreateMenu(ShopMenu shopMenu)
        {
            if (ModelState.IsValid)
            {
                int id = ((Users)Session["user"]).UserNumber;
                var users = db.Users.Find(id);

                string sql = "insert into ShopMenu(UserNumber,ShopNumber,Item,Price)values(@UserNumber,@ShopNumber,@Item,@Price)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",users.UserNumber),
                    new SqlParameter("ShopNumber",shopMenu.ShopNumber),
                    new SqlParameter("Item",shopMenu.Item),
                    new SqlParameter("Price",shopMenu.Price)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopMenu.ShopNumber);
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopTag.UserNumber);
            return View(shopMenu);
        }


        public ActionResult _TagDetail(int id)
        {
            return PartialView(db.ShopTag.Where(m => m.ShopNumber == id).ToList());
        }

        [LoginCheck(id = 1)]
        public ActionResult UserCreateTag()
        {
            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreateTag(ShopTag shopTag)
        {
            var tag = db.ShopTag.Where(m => m.ShopNumber == shopTag.ShopNumber && m.Tag == shopTag.Tag).FirstOrDefault();
            if (tag != null)
            {
                ViewBag.TagCheck = "此標籤已存在，請輸入其他";
            }
            else if (ModelState.IsValid)
            {
                int id = ((Users)Session["user"]).UserNumber;
                var users = db.Users.Find(id);

                string sql = "insert into ShopTag(UserNumber,ShopNumber,Tag)values(@UserNumber,@ShopNumber,@Tag)";
                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",users.UserNumber),
                    new SqlParameter("ShopNumber", shopTag.ShopNumber),
                    new SqlParameter("Tag", shopTag.Tag)
                };
                
                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopTag.ShopNumber);
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopTag.UserNumber);
            return View(shopTag);
        }

        public ActionResult _PayDetail(int id)
        {
            return PartialView(db.ShopPay.Where(m => m.ShopNumber == id).ToList());
        }

        [LoginCheck(id = 1)]
        public ActionResult UserCreatePay()
        {
            ViewBag.Shop = db.Shop.ToList();
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreatePay(ShopPay shopPay)
        {
            var pay = db.ShopPay.Where(m => m.ShopNumber == shopPay.ShopNumber && m.PayNumber == shopPay.PayNumber).FirstOrDefault();
            if(pay!= null)
            {
                ViewBag.PayCheck = "此付款方式已存在";
            }
            else if (ModelState.IsValid)
            {
                    string sql = "insert into ShopPay(ShopNumber,PayNumber)values(@ShopNumber,@PayNumber)";
                    List<SqlParameter> list = new List<SqlParameter>
                    {
                        new SqlParameter("ShopNumber",shopPay.ShopNumber),
                        new SqlParameter("PayNumber",shopPay.PayNumber)
                    };
                    sd.executeSql(sql, list);
                    return RedirectToAction("Index");
            }

            ViewBag.Shop = db.Shop.ToList();
            ViewBag.PayNumber = new SelectList(db.Pay, "PayNumber", "PayType", shopPay.PayNumber);
            return View(shopPay);
        }

        public ActionResult _ImageDetail(int id)
        {
            return PartialView(db.ShopImage.Where(m => m.ShopNumber == id).ToList());
        }

        [LoginCheck(id = 1)]
        public ActionResult UserCreateImage()
        {
            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName");
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreateImage(ShopImage shopImage, HttpPostedFileBase upload)
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
                return RedirectToAction("Index");
            }

            ViewBag.Shop = db.Shop.ToList();
            //ViewBag.ShopNumber = new SelectList(db.Shop, "ShopNumber", "ShopName", shopImage.ShopNumber);
            //ViewBag.UserNumber = new SelectList(db.Users, "UserNumber", "UserNumber", shopImage.UserNumber);
            return View(shopImage);
        }

        public FileContentResult GetShopPhoto(int id)
        {
            var Photo = db.ShopImage.Find(id);
            if (Photo != null)
                return File(Photo.ShopImage1, "image/jpeg");
            return null;

        }

        public FileContentResult GetUserPhoto(int id)
        {
            var photo = db.Users.Find(id);
            if (photo != null)
                return File(photo.UserPhoto, "image/jpeg");
            return null;

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Users users, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Users(UserAccount,UserPassword,UserName,Sex,Birthday,Blockade,UserPhoto,UserDate)" +
                "values(@UserAccount,@UserPassword,@UserName,@Sex,@Birthday,@Blockade,CONVERT(varbinary(max), @UserPhoto),@UserDate)";

                if (upload != null)
                {
                    int filelength = upload.ContentLength;
                    byte[] Myfile = new byte[filelength];
                    upload.InputStream.Read(Myfile, 0, filelength);
                    users.UserPhoto = Myfile;
                }
                users.UserDate = DateTime.Now;

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserAccount",users.UserAccount),
                    new SqlParameter("UserPassword",users.UserPassword),
                    new SqlParameter("UserName",users.UserName),
                    new SqlParameter("Sex",users.Sex),
                    new SqlParameter("Birthday",users.Birthday),
                    new SqlParameter("Blockade",users.Blockade),
                    new SqlParameter("UserPhoto",users.UserPhoto),
                    new SqlParameter("UserDate",users.UserDate)
                };

                sd.executeSql(sql, list);

                return RedirectToAction("Login");
            }

            return View(users);
        }

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {

            var user = db.Users.Where(m => m.UserAccount == vMLogin.Account && m.UserPassword == vMLogin.Password).FirstOrDefault();
            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤";
                return View(vMLogin);
            }
            else if(user.Blockade == true)
            {
                ViewBag.ErrMsg = "此帳戶已被封鎖";
                return View(vMLogin);
            }
            
            Session["user"] = user;
            Session["userBlock"] = user.Blockade;
            return RedirectToAction("Index");

        }

        [LoginCheck(id = 1)]
        public ActionResult UserIndex()
        {
            int id = ((Users)Session["user"]).UserNumber;
            var users = db.Users.Find(id);

            return View(users);
        }


        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }

   
        public ActionResult _MySave()
        {
            return PartialView();
        }


        public ActionResult PostShop()
        {
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostShop(PostShop postShop)
        {
            if (ModelState.IsValid)
            {
                int id = ((Users)Session["user"]).UserNumber;
                var users = db.Users.Find(id);

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
                    new SqlParameter("UserNumber",users.UserNumber),
                    new SqlParameter("AdmNumber",postShop.AdmNumber)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");

            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", postShop.AdmNumber);
            return View(postShop);
        }

        public ActionResult Suggest()
        {
            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suggest(Suggest suggest)
        {
            if (ModelState.IsValid)
            {
                int id = ((Users)Session["user"]).UserNumber;
                var users = db.Users.Find(id);

                string sql = "insert into Suggest(UserNumber,Suggest,AdmNumber)values(@UserNumber,@Suggest,@AdmNumber)";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserNumber",users.UserNumber),
                    new SqlParameter("Suggest",suggest.Suggest1),
                    new SqlParameter("AdmNumber",suggest.AdmNumber)
                };

                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }

            ViewBag.AdmNumber = new SelectList(db.Adm, "AdmNumber", "AdmNumber", suggest.AdmNumber);
            return View(suggest);
        }

    }

}
