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

            // id 1 == Women, 2 == Men, 3 == Girls, 4 == Boys

            var dbGodtSkodd = new DBGodtSkodd();
            List<Product> products;

            if (id == 1 || id == 2 || id == 3 || id == 4)
            {
                String gender = "";

                if (id == 1)
                    gender = "Women";
                else if (id == 2)
                    gender = "Men";
                else if (id == 3)
                    gender = "Girls";
                else // id == 4
                    gender = "Boys";

                products = dbGodtSkodd.ListProductsOfGender(gender);
            }
            else
            {
                products = dbGodtSkodd.ListAllProducts();
            }

            return View(products);
        }
        
        public JsonResult Login(String username, String password)
        {
            // EV. FÅ USER FRA DBGODTSKODD OG SENDE DENNE I JSON-FORMAT TIL AJAX-KALLET?

            // checking login

            LoginUser loginUser = new LoginUser();
            loginUser.userName = username;
            loginUser.password = password;

            var dbGodtSkodd = new DBGodtSkodd();

            if (dbGodtSkodd.UserInDb(loginUser))
            {
                // yes username and password is OK
                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;

                JsonResult jsonOutput = Json(loginUser, JsonRequestBehavior.AllowGet);
                return jsonOutput;
                
                //return RedirectToAction("Index");
            }
            else
            {
                // no
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                // return RedirectToAction("Index");
                return null;
            }
            
            //return RedirectToAction("Index");
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