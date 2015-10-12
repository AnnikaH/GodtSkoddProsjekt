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
            return true;
        }


        int produktTeller = 0;
        String[] name = new String[103];
        double[] price = new double[103];
        int[] size = new int[103];
        String[] Color = new String[103];
        String[] Material = new String[103];
        String[] Brand = new String[103];
        String[] URL = new String[103];
        String[] Gender = new String[103];
        String[] TYPE = new String[103];
        public static int teller = 0;

        public void CreateAllProducts()
        {
            teller = 0;
            InsertNewProduct("Woman Rain Boots", 79, 9001, "Black", "Rubber", "OwnShoe", "/Content/Images/Women/Boots/1.jpg", "Women", "Boots", teller++);
            InsertNewProduct("Ownshoe Women Rubber Polka Dots Mid Calf Wellies Color Dots Rainboots", 50, 9002, "Black/White", "Rubber", "OwnShoe", "/Content/Images/Women/Boots/2.jpg", "Women", "Boots", teller++);
            InsertNewProduct("Salvatore Ferragamo Black rubber gancio clasp rain boots", 360, 9003, "Black", "Rubber", "Salvatore Ferragamo", "/Content/Images/Women/Boots/3.jpg", "Women", "Boots", teller++);

            LagOgSettInn();
        }

        public void InsertNewProduct(String N, double P, int S, String C, String M, String B, String U, String G, String T, int i)
        {
            name[i] = N;
            price[i] = P;
            size[i] = S;
            Color[i] = C;
            Material[i] = M;
            Brand[i] = B;
            URL[i] = U;
            Gender[i] = G;
            TYPE[i] = T;
        }

        public void LagOgSettInn()
        {

            DBGodtSkodd db = new DBGodtSkodd();
            for (int i = 0; i < teller; i++)
            {
                Product temp = new Product();
                temp.name = name[i];
                temp.price = price[i];
                temp.size = size[i];
                temp.color = Color[i];
                temp.material = Material[i];
                temp.brand = Brand[i];
                temp.url = URL[i];
                temp.gender = Gender[i];
                temp.type = TYPE[i];
                db.CreateProduct(temp);
            }
        }
    }
}