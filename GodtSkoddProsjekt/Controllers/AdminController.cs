using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GodtSkoddProsjekt.Models;

// Use this for adding products (shoes) among other things?

namespace GodtSkoddProsjekt.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool insertOK = dbGodtSkodd.CreateProduct(product);

                if (insertOK)
                    return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            Product product = dbGodtSkodd.GetProduct(id);
            return View(product);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var dbGodtSkodd = new DBGodtSkodd();
                bool changeOK = dbGodtSkodd.EditProduct(id, product);

                if (changeOK)
                    return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            Product product = dbGodtSkodd.GetProduct(id);
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            bool deleteOK = dbGodtSkodd.DeleteProduct(id);

            if (deleteOK)
                return RedirectToAction("Index");

            return View();
        }

        // CREATE PRODUCTS BACKEND:

        public bool CreateProductBackend(Product product)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            return dbGodtSkodd.CreateProduct(product);
        }

        public void CreateAllProducts()
        {
            //String[] brandsarray = { "", "", ""; "", "", "" };


            //string[][] products = new String[9][]();


        }
    }
}