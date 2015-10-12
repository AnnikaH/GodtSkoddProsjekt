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
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            User user = dbGodtSkodd.GetUser(id);
            return View(user);
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
                    return RedirectToAction("Index");
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
            var dbGodtSkodd = new DBGodtSkodd();
            bool deleteOK = dbGodtSkodd.DeleteUser(id, user);

            if (deleteOK)
                return RedirectToAction("Liste");

            return View();
        }
    }
}
