using Subject.Models;
using Subject.ViewModels;
using System;
using System.Collections.Generic;
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

            return PartialView(shop);
        }

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

            Session["user"] = user;
            Session["UserName"] = user.UserName;
            Session["Sex"] = user.Sex;
            Session["Birthday"] = user.Birthday;
            Session["Age"] = user.Age;
            Session["UserPhoto"] = user.UserPhoto;
            //return RedirectToAction("Profile", new { id = user.UserNumber });
            return RedirectToAction("Index");

        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }


        public ActionResult Profile()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }

}
