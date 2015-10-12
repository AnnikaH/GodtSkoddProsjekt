using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GodtSkoddProsjekt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // tester:
            // var dbGodtSkodd = new DBGodtSkodd();
            // dbGodtSkodd.test();
            // tester til hit

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginUser user)
        {
            // checking log in

            var dbGodtSkodd = new DBGodtSkodd();

            if (dbGodtSkodd.UserInDb(user))
            {
                // yes username and password is OK
                Session["LoggedIn"] = true;
                ViewBag.Innlogget = true;
                // return RedirectToAction("Index");
            }
            else
            {
                // no
                Session["LoggedIn"] = false;
                ViewBag.Innlogget = false;
                // return RedirectToAction("Index");
            }

            // Can use ViewBag in the view!

            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}