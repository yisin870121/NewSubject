using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subject.Models;
using Subject.ViewModels;

namespace Subject.Controllers
{
    public class UsersController : Controller
    {
        private SpecialSubjectEntities db = new SpecialSubjectEntities();
        SetData sd = new SetData();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }

        // GET: Users/Create
        public ActionResult _Create()
        {
            return PartialView();
        }

        // POST: Users/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into Users(UserAccount,UserPassword,UserName,Sex,Birthday,Blockade)" +
                "values(@UserAccount,@UserPassword,@UserName,@Sex,@Birthday,@Blockade)";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("UserAccount",users.UserAccount),
                    new SqlParameter("UserPassword",users.UserPassword),
                    new SqlParameter("UserName",users.UserName),
                    new SqlParameter("Sex",users.Sex),
                    new SqlParameter("Birthday",users.Birthday),
                    new SqlParameter("Blockade",users.Blockade)
                };

                sd.executeSql(sql, list);

                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }

        // POST: Users/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users)
        {
            string sql = "update Users set UserName=@UserName,Birthday=@Birthday,Blockade=@Blockade where UserNumber=@UserNumber";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("UserNumber",users.UserNumber),
                new SqlParameter("UserName",users.UserName),
                new SqlParameter("Birthday",users.Birthday),
                new SqlParameter("Blockade",users.Blockade)
            };

            try
            { 
                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View(users);
            }
        }

        // GET: Users/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }

        //POST: Users/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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

        public ActionResult Block()
        {
            return View(db.Users.Where(m => m.Blockade).ToList());
        }



        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(VMLogin vMLogin)
        //{
        //    string password = BR.getHashPassword(vMLogin.Password);

        //    var user = db.Users.Where(m => m.UserAccount == vMLogin.Account && m.UserPassword == vMLogin.Password).FirstOrDefault();

        //    if (user == null)
        //    {
        //        ViewBag.ErrMsg = "帳號或密碼有誤";
        //        return View(vMLogin);
        //    }
        //    Session["user"] = user;
        //    return RedirectToAction("Profile", new { id = user.UserNumber });
        //}

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
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index","Home");
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









        //public ActionResult AfterLogin(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Users users = db.Users.Find(id);
        //    if (users == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(users);
        //}


        //public ActionResult AfterLogin(VMLogin vMLogin)
        //{
        //    var user = db.Users.Where(m => m.UserAccount == vMLogin.Account && m.UserPassword == vMLogin.Password).FirstOrDefault();
        //    return View(user);
        //}


    }
}
