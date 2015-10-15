﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GodtSkoddProsjekt.Models;

namespace GodtSkoddProsjekt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
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
                ViewBag.LoggedIn = (bool) Session["LoggedIn"]; // HUSK: DEN MÅ CASTES
            }

            // Return 9 "top" products?:

            var dbGodtSkodd = new DBGodtSkodd();

            List<Product> products = dbGodtSkodd.ListAllProducts();

            return View(products);
        }

        public ActionResult ListProductsOfType(int id)
        {
            // 1 == Women, 2 == Men, 3 == Girls, 4 == Boys

            if(id == 1 || id == 2 || id == 3 || id == 4)
            {
                String type = "";

                if (id == 1)
                    type = "Women";
                else if (id == 2)
                    type = "Men";
                else if (id == 3)
                    type = "Girls";
                else // id == 4
                    type = "Boys";

                var dbGodtSkodd = new DBGodtSkodd();

                List<Product> products = dbGodtSkodd.ListProductsOfType(type);

                return View(products);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser user)
        {
            // checking login

            var dbGodtSkodd = new DBGodtSkodd();

            if (dbGodtSkodd.UserInDb(user))
            {
                // yes username and password is OK
                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;
                // return RedirectToAction("Index");
            }
            else
            {
                // no
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                // return RedirectToAction("Index");
            }

            // Can use ViewBag in the view!

            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        /*public ActionResult Details(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            Product product = dbGodtSkodd.GetProduct(id);
            return View(product);
        }*/
    }
}