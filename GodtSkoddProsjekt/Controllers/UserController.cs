using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GodtSkoddProsjekt.Models;
using System.Web.Routing;

namespace GodtSkoddProsjekt.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int? id)
        {
            // Checking login:
            if (Session["LoggedIn"] == null)
            {
                // da definerer vi den og setter den til false
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false; // oppdaterer denne også!
            }
            else
            {
                // vil så hente ut statusen til session'en og legge denne over i ViewBag'en:
                ViewBag.LoggedIn = (bool) Session["LoggedIn"]; // Husk: Må castes!
            }

            bool loggedIn = (bool)Session["LoggedIn"];

            if (loggedIn)
            {
                if(id.HasValue && ((int) Session["UserId"] == id))  // you get thrown out if you try to access info to another user
                {
                    var dbGodtSkodd = new DBGodtSkodd();

                    int idNumber = (int)id;
                    User user = dbGodtSkodd.GetUser(idNumber);

                    if (user != null)
                        return View(user);
                    else
                    {
                        // (since no user was found):
                        Session["LoggedIn"] = false;
                        ViewBag.LoggedIn = false;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            Session["LoggedIn"] = false;
            ViewBag.LoggedIn = false;
            return RedirectToAction("Index", "Home");
        }

        /* GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Add security if going to use
            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
        }*/

        public ActionResult OrderDetails(int id)
        {
            // Checking login:
            if (Session["LoggedIn"] == null)
            {
                // da definerer vi den og setter den til false
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false; // oppdaterer denne også!
            }
            else
            {
                // vil så hente ut statusen til session'en og legge denne over i ViewBag'en:
                ViewBag.LoggedIn = (bool)Session["LoggedIn"]; // Husk: Må castes!
            }

            bool loggedIn = (bool)Session["LoggedIn"];

            if (loggedIn && ((int) Session["UserId"] == id))    // you get thrown out if you try to access info to another user
            {
                // get user id
                var dbGodtSkodd = new DBGodtSkodd();

                List<Order> orders = dbGodtSkodd.GetOrdersForUser(id);

                if (orders != null)
                    return View(orders);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool insertOK = dbGodtSkodd.CreateUser(user);

                if (insertOK)
                {
                    Session["LoggedIn"] = true;
                    ViewBag.LoggedIn = true;

                    LoginUser loginUser = new LoginUser();
                    loginUser.userName = user.userName;
                    loginUser.password = user.password;

                    Session["UserId"] = dbGodtSkodd.GetUserIdInDB(loginUser);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Checking login:
            if (Session["LoggedIn"] == null)
            {
                // da definerer vi den og setter den til false
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false; // oppdaterer denne også!
            }
            else
            {
                // vil så hente ut statusen til session'en og legge denne over i ViewBag'en:
                ViewBag.LoggedIn = (bool)Session["LoggedIn"]; // Husk: Må castes!
            }

            bool loggedIn = (bool)Session["LoggedIn"];

            if (loggedIn && ((int) Session["UserId"] == id))    // you get thrown out if you try to access info to another user
            {
                var dbGodtSkodd = new DBGodtSkodd();
                User user = dbGodtSkodd.GetUser(id);
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            // Checking login:
            if (Session["LoggedIn"] == null)
            {
                // da definerer vi den og setter den til false
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false; // oppdaterer denne også!
            }
            else
            {
                // vil så hente ut statusen til session'en og legge denne over i ViewBag'en:
                ViewBag.LoggedIn = (bool)Session["LoggedIn"]; // Husk: Må castes!
            }

            bool loggedIn = (bool)Session["LoggedIn"];

            if (loggedIn)
            {
                if(ModelState.IsValid)
                {
                    var dbGodtSkodd = new DBGodtSkodd();
                    bool changeOK = dbGodtSkodd.EditUser(id, user);

                    if (changeOK)
                        return RedirectToAction("Index", new { id = id });
                }

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        /* GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Add security if going to use

            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
        }*/

        /* POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)
        {
            // Add security if going to use

            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool deleteOK = dbGodtSkodd.DeleteUser(id, user);

                if (deleteOK)
                    return RedirectToAction("Index");
            }

            return View();
        }*/
    }
}