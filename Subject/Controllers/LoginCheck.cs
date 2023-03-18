using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subject.Controllers
{
    public class LoginCheck : ActionFilterAttribute
    {
        public bool flag = true;

        public short id = 2;

        void AdmLoginState(HttpContext context)
        {
            if (context.Session["adm"] == null)
            {
                context.Response.Redirect("/Adms/Login");
            }
        }

        void UserLoginState(HttpContext context)
        {
            if (context.Session["user"] == null || context.Session["userBlock"] is true)
            {
                context.Response.Redirect("/Home/Login");
            }
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (flag)
            {
                HttpContext context = HttpContext.Current;

                if (id == 1)
                    UserLoginState(context);
                else
                    AdmLoginState(context);


            }
        }
    }
}