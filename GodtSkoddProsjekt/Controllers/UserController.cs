using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GodtSkoddProsjekt.Models;

namespace GodtSkoddProsjekt.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int? id)  // int? id > i metoden: sende med id.Value til dbGodtSkodd, best å ikke ta inn en id her (sikkerhet)
        {
            // sjekk om bruker innlogget (og få tak i id?)

            // tester:
            /*
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
            }*/

            if (Session["LoggedIn"] != null)   // må teste på om den er satt eller ikke!
            {
                bool loggedIn = (bool)Session["LoggedIn"];

                if (loggedIn)
                {
                    if(id.HasValue)
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
                            return Redirect("Home/Index");
                        }
                    }
                    else
                    {
                        // (since no user was found):
                        Session["LoggedIn"] = false;
                        ViewBag.LoggedIn = false;
                        return Redirect("Home/Index");
                    }
                }
                else
                {
                    // just in case:
                    Session["LoggedIn"] = false;
                    ViewBag.LoggedIn = false;
                    return Redirect("Home/Index");
                }
            }
            else
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                return Redirect("Home/Index");
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
        }

        public ActionResult OrderDetails(int id)
        {
            // get user id
            var dbGodtSkodd = new DBGodtSkodd();

            List<Order> orders = dbGodtSkodd.GetOrdersForUser(id);

            if (orders != null)
                return View(orders);
            else
                //return RedirectToAction("Index");
                return View();
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
                    return RedirectToAction("Index");   // Går til Min side
                }
            }

            return View();
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool changeOK = dbGodtSkodd.EditUser(id, user);

                if (changeOK)
                    return RedirectToAction("Index");
            }

            return View();
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)   // istedenfor User: FormCollection collection ?
        {
            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool deleteOK = dbGodtSkodd.DeleteUser(id, user);

                if (deleteOK)
                    return RedirectToAction("Index");
            }

            return View();
        }
    }
}