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

            InsertNewProduct("Traktor Regnstøvel", 200,1, "Rød", "Gummi", "Amazon", "/Content/Images/Boys/Boots/1.jpg", "Gutter", "Støvler", teller++);
            InsertNewProduct("Frosk Regnstøvel", 290, 1,"Grønn", "Gummi", "Amazon", "/Content/Images/Boys/Boots/2.jpg", "Gutter", "Støvler", teller++);
            InsertNewProduct("Viking Regnstøvel", 230, 1,"Blå", "Gummi", "Viking", "/Content/Images/Boys/Boots/3.jpg", "Gutter", "Støvler", teller++);
            InsertNewProduct("Hunter Regnstøvel", 800, 1,"Blå/Grå", "Gummi", "Hunter", "/Content/Images/Boys/Boots/4.jpg", "Gutter", "Støvler", teller++);
            InsertNewProduct("Byggmester Bob Støvel", 450, 1,"Blå", "Gummi", "BobBuilder", "/Content/Images/Boys/Boots/5.jpg", "Gutter", "Støvler", teller++);

            InsertNewProduct("Pensko for Gutter", 370, 1,"Hvit/Svart", "Skinn", "AliExpress", "/Content/Images/Boys/DressShoes/1.jpg", "Gutter", "Pensko", teller++);
            InsertNewProduct("Pensko", 350, 1,"Svart/Sølv", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/2.jpg", "Gutter", "Pensko", teller++);
            InsertNewProduct("Formelle Skinn Sko", 500, 1,"Svart", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/3.jpg", "Gutter", "Pensko", teller++);
            InsertNewProduct("Formelle Sko", 500, 1,"Svart", "Skinn", "Japanned", "/Content/Images/Boys/DressShoes/4.jpg", "Gutter", "Pensko", teller++);
            InsertNewProduct("Formelle Sko for Gutter", 390, 1,"Svart", "Skinn", "ToysRUs", "/Content/Images/Boys/DressShoes/5.jpg", "Gutter", "Pensko", teller++);

            InsertNewProduct("Sommer Brand Sandaler for Gutter", 390,1, "Hvit", "Mesh", "Uovo", "/Content/Images/Boys/Sandals/1.jpg", "Gutter", "Sandaler", teller++);
            InsertNewProduct("Baby Gå Sko", 550, 1,"Hvit/Blå", "Skinn", "Caroch", "/Content/Images/Boys/Sandals/2.jpg", "Gutter", "Sandaler", teller++);
            InsertNewProduct("Mikke Mus Sandaler", 150, 1,"Blå", "Mesh", "Disney", "/Content/Images/Boys/Sandals/3.jpg", "Gutter", "Sandaler", teller++);
            InsertNewProduct("Relix Sandaler", 590, 1,"Svart", "Mesh", "Skechers", "/Content/Images/Boys/Sandals/4.jpg", "Gutter", "Sandaler", teller++);
            InsertNewProduct("Coga Sport Sandaler for Barn", 350, 1,"Blå", "Mesh", "Coga", "/Content/Images/Boys/Sandals/5.jpg", "Gutter", "Sandaler", teller++);

            InsertNewProduct("Joggesko", 350, 1,"Blå", "Mesh", "Jtengda", "/Content/Images/Boys/Sneakers/1.jpg", "Gutter", "Joggesko", teller++);
            InsertNewProduct("Air Cushion Joggesko", 390, 1,"Blå", "Mesh", "KanlBear", "/Content/Images/Boys/Sneakers/2.jpg", "Gutter", "Joggesko", teller++);
            InsertNewProduct("Totoho Lite", 300, 1,"Svart", "Mesh", "Totoho", "/Content/Images/Boys/Sneakers/3.jpg", "Gutter", "Joggesko", teller++);
            InsertNewProduct("Y-8 Joggesko", 490, 1,"Svart", "Mesh", "Miqixz", "/Content/Images/Boys/Sneakers/4.jpg", "Gutter", "Joggesko", teller++);
            InsertNewProduct("Panther Fotball Sko", 500, 1,"Grønn/Gul", "Mesh", "TieBao", "/Content/Images/Boys/Sneakers/5.jpg", "Gutter", "Joggesko", teller++);

            InsertNewProduct("Vinter Brand Skinn Støvel", 450, 1,"Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/1.jpg", "Gutter", "Vintersko", teller++);
            InsertNewProduct("Vinter Snø Sko", 500, 1,"Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/2.jpg", "Gutter", "Vintersko", teller++);
            InsertNewProduct("PU Skinn Snø Støvel", 130, 1,"Grønn", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/3.jpg", "Gutter", "Vintersko", teller++);
            InsertNewProduct("Hongda CowHide Sko", 380, 1,"Grønn", "Skinn", "Hongda", "/Content/Images/Boys/WinterShoes/4.jpg", "Gutter", "Vintersko", teller++);
            InsertNewProduct("Vinter Utesko", 150, 1,"Grønn", "Gummi", "AliExpress", "/Content/Images/Boys/WinterShoes/5.jpg", "Gutter", "Vintersko", teller++);

            InsertNewProduct("Dora The Explorer Regnstøvel", 500, 1,"Rosa", "Gummi", "Dora The Explorer", "/Content/Images/Girls/Boots/1.jpg", "Jenter", "Støvel", teller++);
            InsertNewProduct("Minni Mus Regnstøvel", 390, 1,"Rød", "Gummi", "Disney", "/Content/Images/Girls/Boots/2.jpg", "Jenter", "Støvel", teller++);
            InsertNewProduct("Frozen Regnstøvel", 500, 1,"Grønn", "Gummi", "Disney", "/Content/Images/Girls/Boots/3.jpg", "Jenter", "Støvel", teller++);
            InsertNewProduct("Kanin Regnstøvel", 290, 1,"Rosa", "Gummi", "LostLands", "/Content/Images/Girls/Boots/4.jpg", "Jenter", "Støvel", teller++);
            InsertNewProduct("Hello Kitty Regnstøvel", 450, 1,"Rosa", "Gummi", "Hello Kitty", "/Content/Images/Girls/Boots/5.jpg", "Jenter", "Støvel", teller++);

            InsertNewProduct("Formelle Pensko", 600, 1,"Rosa", "Skinn", "The Bay", "/Content/Images/Girls/DressShoes/1.jpg", "Jenter", "Pensko", teller++);
            InsertNewProduct("Pensko", 150, 1,"Gull", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/2.jpg", "Jenter", "Pensko", teller++);
            InsertNewProduct("Blomstersko", 590,1, "Hvit", "Skinn", "LM83", "/Content/Images/Girls/DressShoes/3.jpg", "Jenter", "Pensko", teller++);
            InsertNewProduct("Formelle Baby Pensko", 100,1, "Sølv", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/5.jpg", "Jenter", "Pensko", teller++);
            InsertNewProduct("Pensko", 450, 1,"Svart", "Skinn", "JoyLand", "/Content/Images/Girls/DressShoes/5.jpg", "Jenter", "Pensko", teller++);

            InsertNewProduct("Blomster Sandaler", 390,1, "Rød", "Skinn", "The Bay", "/Content/Images/Girls/Sandals/1.jpg", "Jenter", "Sandaler", teller++);
            InsertNewProduct("Flower Sandaler", 150, 1,"Hvit", "Syntetisk Skinn", "Monsoon", "/Content/Images/Girls/Sandals/2.jpg", "Jenter", "Sandaler", teller++);
            InsertNewProduct("Princess BowTie", 450, 1,"Rosa", "Skinn", "DCamp", "/Content/Images/Girls/Sandals/3.jpg", "Jenter", "Sandaler", teller++);
            InsertNewProduct("Barnesandaler", 550, 1,"Grå", "Mesh", "Gzzhl", "/Content/Images/Girls/Sandals/4.jpg", "Jenter", "Sandaler", teller++);
            InsertNewProduct("Jewelled Sandaler", 290, 1,"Rosa", "Syntetisk Skinn", "AliCDN", "/Content/Images/Girls/Sandals/5.jpg", "Jenter", "Sandaler", teller++);

            InsertNewProduct("Dansesko", 500, 1,"Lilla", "Mesh", "Uovo", "/Content/Images/Girls/Joggesko/1.jpg", "Jenter", "Joggesko", teller++);
            InsertNewProduct("Originals ZX flux", 700,1, "Lilla", "Mesh", "Adidas", "/Content/Images/Girls/Joggesko/2.jpg", "Jenter", "Joggesko", teller++);
            InsertNewProduct("Eclipse Løpesko", 490, 1,"Rosa", "Mesh", "Eclipse", "/Content/Images/Girls/Joggesko/3.jpg", "Jenter", "Joggesko", teller++);
            InsertNewProduct("Outdoor Sport Sko", 250, 1,"Rosa", "Mesh", "AliExpress", "/Content/Images/Girls/Joggesko/4.jpg", "Jenter", "Joggesko", teller++);
            InsertNewProduct("GT 1000-4", 550, 1,"Rosa", "Mesh", "Asics", "/Content/Images/Girls/Joggesko/5.jpg", "Jenter", "Joggesko", teller++);

            InsertNewProduct("Lobbing Ball Snø Sko", 100,1, "Rosa", "Plush", "AliExpress", "/Content/Images/Girls/WinterShoes/1.jpg", "Jenter", "Vintersko", teller++);
            InsertNewProduct("Candy Thermal Støvel", 300, 1,"Lilla", "Plush", "Coga", "/Content/Images/Girls/WinterShoes/2.jpg", "Jenter", "Vintersko", teller++);
            InsertNewProduct("Vinter Støvel", 450, 1,"Rosa", "Buffalo Hide", "AliExpress", "/Content/Images/Girls/WinterShoes/3.jpg", "Jenter", "Vintersko", teller++);
            InsertNewProduct("Rabbit Bowknot", 250, 1,"Rosa", "Skinn", "AliExpress", "/Content/Images/Girls/WinterShoes/4.jpg", "Jenter", "Vintersko", teller++);
            InsertNewProduct("Juicy Couture", 2000, 1,"Rosa", "Velour", "Juicy", "/Content/Images/Girls/WinterShoes/5.jpg", "Jenter", "Vintersko", teller++);

            InsertNewProduct("Stål-Shank Regnstøvel for Menn", 200, 1,"Svart", "Gummi", "Walmart", "/Content/Images/Men/Boots/1.jpg", "Menn", "Støvel", teller++);
            InsertNewProduct("Male Martin Regnsko", 390, 1,"Svart", "Gummi", "Martin", "/Content/Images/Men/Boots/2.jpg", "Menn", "Støvel", teller++);
            InsertNewProduct("Tretorn Strala Regn Støvel", 600, 1,"Svart", "Gummi", "Tretorn", "/Content/Images/Men/Boots/3.jpg", "Menn", "Støvel", teller++);
            InsertNewProduct("Economy Plain Toe Kne støvel", 180,1, "Svart", "Gummi", "Tingley", "/Content/Images/Men/Boots/4.jpg", "Menn", "Støvel", teller++);
            InsertNewProduct("Hot Designers Gummistøvel", 390, 1,"Svart", "Gummi", "Warrior", "/Content/Images/Men/Boots/5.jpg", "Menn", "Støvel", teller++);

            InsertNewProduct("Bata Formell Sko", 8990, 1,"Svart", "Skinn", "Bata", "/Content/Images/Men/DressShoes/1.jpg", "Menn", "Pensko", teller++);
            InsertNewProduct("Sir Corbett Formelle Sko", 6000, 1,"Svart", "Syntetisk Skinn", "Sir Corbett", "/Content/Images/Men/DressShoes/2.jpg", "Menn", "Pensko", teller++);
            InsertNewProduct("Fellini Formelle Herresko", 3249, 1,"Brun", "Skinn", "Fellini", "/Content/Images/Men/DressShoes/3.jpg", "Menn", "Pensko", teller++);
            InsertNewProduct("Michael Toschi Gala Skinn Sko", 5950, 1,"Svart", "Leather", "Michael Toschi", "/Content/Images/Men/DressShoes/4.jpg", "Menn", "Pensko", teller++);
            InsertNewProduct("Roxxii Herresko", 8990, 1,"Svart", "Faux Patent Skinn", "Roxxii", "/Content/Images/Men/DressShoes/5.jpg", "Menn", "Pensko", teller++);

            InsertNewProduct("Orvis Sandaler", 990, 1,"Brun", "Skinn", "Orvis", "/Content/Images/Men/Sandals/1.jpg", "Menn", "Sandaler", teller++);
            InsertNewProduct("Sandaler", 859, 1,"Brun", "Faux Skinn", "Bacca Bucci", "/Content/Images/Men/Sandals/2.jpg", "Menn", "Sandaler", teller++);
            InsertNewProduct("Docker Marin Fisherman Sandaler", 750, 1,"Brun", "Skinn", "Docker Marin", "/Content/Images/Men/Sandals/3.jpg", "Menn", "Sandaler", teller++);
            InsertNewProduct("Supreme Equipt Relax Fit", 300, 1,"Svart", "Mesh", "Sketchers", "/Content/Images/Men/Sandals/4.jpg", "Menn", "Sandaler", teller++);
            InsertNewProduct("Grønne Herre Sandaler", 1500, 1,"Grønn", "Faux Skinn", "Weinbrenner", "/Content/Images/Men/Sandals/5.jpg", "Menn", "Sandaler", teller++);

            InsertNewProduct("TravelWalker", 650,1, "Black", "Mesh", "Kroten", "/Content/Images/Men/Sneakers/1.jpg", "Menn", "Joggesko", teller++);
            InsertNewProduct("GT 1000 Løpesko", 990,1, "Svart/Grønn", "Mesh", "Asics", "/Content/Images/Men/Sneakers/2.jpg", "Menn", "Joggesko", teller++);
            InsertNewProduct("Trail Løpesko", 790,1, "Grå", "Mesh", "New Balance", "/Content/Images/Men/Sneakers/3.jpg", "Menn", "Joggesko", teller++);
            InsertNewProduct("Track og Løpesko", 990, 1,"Svart", "Mesh", "Adidas", "/Content/Images/Men/Sneakers/4.jpg", "Menn", "Joggesko", teller++);
            InsertNewProduct("Atletisk Sko", 500, 1,"Hvit", "Mesh", "Man", "/Content/Images/Men/Sneakers/5.jpg", "Menn", "Joggesko", teller++);
            InsertNewProduct("3.0 V5 Løpesko", 890, 1,"Blå", "Mesh", "Nike", "/Content/Images/Men/Sneakers/6.jpg", "Menn", "Joggesko", teller++);

            InsertNewProduct("Erkek Boot Modelleri", 2490, 1,"Brun", "Skinn", "Erkek", "/Content/Images/Men/WinterShoes/1.jpg", "Menn", "Vintersko", teller++);
            InsertNewProduct("Caribou Boots", 1300, 1,"Svart", "Nylon", "Sorel", "/Content/Images/Men/WinterShoes/2.jpg", "Menn", "Vintersko", teller++);
            InsertNewProduct("Classic Vintage Herresko", 950, 1,"Brun", "Skinn", "Doozoo", "/Content/Images/Men/WinterShoes/3.jpg", "Menn", "Vintersko", teller++);
            InsertNewProduct("Jobbsko for Herrer", 400, 1,"Brun", "Cow Split", "AliExpress", "/Content/Images/Men/WinterShoes/4.jpg", "Menn", "Vintersko", teller++);
            InsertNewProduct("Shearling Sko", 1050, 1,"Brun", "Skinn", "LL Bean", "/Content/Images/Men/WinterShoes/5.jpg", "Menn", "Vintersko", teller++);

            InsertNewProduct("Regnstøvel for Kvinner", 790, 1,"Svart", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/1.jpg", "Kvinner", "Støvler", teller++);
            InsertNewProduct("Ownshoe Regnstøvel for kvinner", 500, 1,"Svart/Hvit", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/2.jpg", "Kvinner", "Støvler", teller++);
            InsertNewProduct("Salvatore Ferragamo Regnstøvel", 3600,1, "Svart", "Gummi", "Salvatore Ferragamo", "/Content/Images/Women/Boots/3.jpg", "Kvinner", "Støvler", teller++);
            InsertNewProduct("Hunter Støvel", 1250,1, "Svart", "Gummi", "Hunter", "/Content/Images/Women/Boots/4.jpg", "Kvinner", "Støvler", teller++);
            InsertNewProduct("Dasko Vail Støvel", 1150,1, "Blå", "Gummi", "Dasko Vail", "/Content/Images/Women/Boots/5.jpg", "Kvinner", "Støvler", teller++);

            InsertNewProduct("Rebeca Platform Sko", 8490, 1,"Blå", "Suede", "Jessica Simpson", "/Content/Images/Women/DressShoes/1.jpg", "Kvinner", "Pensko", teller++);
            InsertNewProduct("Ruched Saueskinn Fest Sko", 980,1, "Svart", "Saueskinn", "Ruched", "/Content/Images/Women/DressShoes/2.jpg", "Kvinner", "Pensko", teller++);
            InsertNewProduct("Quirkin Formelle Damesko", 500,1, "Hvit", "Syntetisk Skinn", "Quirkin", "/Content/Images/Women/DressShoes/3.jpg", "Kvinner", "Pensko", teller++);
            InsertNewProduct("Formelle Platform Sko", 750, 1,"Svart", "Suede", "Europe American Express", "/Content/Images/Women/DressShoes/4.jpg", "Kvinner", "Pensko", teller++);
            InsertNewProduct("Platform Sandaler", 1100, 1,"Svart", "Suede", "Traditionals", "/Content/Images/Women/DressShoes/5.jpg", "Kvinner", "Pensko", teller++);
            InsertNewProduct("Pensko for Kvinner", 1500,1, "Rosa", "Suede", "WUFashion", "/Content/Images/Women/DressShoes/6.jpg", "Kvinner", "Pensko", teller++);

            InsertNewProduct("Platform Sandaler", 850, 1,"Brun", "Skinn", "Laurent", "/Content/Images/Women/Sandals/1.jpg", "Kvinner", "Sandaler", teller++);
            InsertNewProduct("Åpne Sandals", 200, 1,"Hvit", "Syntetisk Skinn", "Ahuh", "/Content/Images/Women/Sandals/2.jpg", "Kvinner", "Sandaler", teller++);
            InsertNewProduct("Broderte Sandaler", 250,1, "Rød", "Vev", "Polyvore", "/Content/Images/Women/Sandals/3.jpg", "Kvinner", "Sandaler", teller++);
            InsertNewProduct("Gladiator Sandaler", 300, 1,"Brun", "Syntetisk Skinn", "The Bay", "/Content/Images/Women/Sandals/4.jpg", "Kvinner", "Sandaler", teller++);
            InsertNewProduct("Rungsted Sandaler", 1000, 1,"Svart", "Skinn", "Ecco", "/Content/Images/Women/Sandals/5.jpg", "Kvinner", "Sandaler", teller++);

            InsertNewProduct("Atletisk Sko", 350, 1,"Blå/Rosa", "Mesh", "Skechers", "/Content/Images/Women/Sneakers/1.jpg", "Kvinner", "Joggesko", teller++);
            InsertNewProduct("Gel Kayano 18 Løpesko", 950, 1,"Svart", "Mesh", "Asics", "/Content/Images/Women/Sneakers/2.jpg", "Kvinner", "Joggesko", teller++);
            InsertNewProduct("Puma Løpesko", 690, 1,"Grå", "Mesh", "Puma", "/Content/Images/Women/Sneakers/3.jpg", "Kvinner", "Joggesko", teller++);
            InsertNewProduct("Riaze Løpesko", 790, 1,"Svart", "Mesh", "Puma", "/Content/Images/Women/Sneakers/4.jpg", "Kvinner", "Joggesko", teller++);
            InsertNewProduct("XR Mission Løpesko", 900,1, "Blå", "Mesh", "Salomon", "/Content/Images/Women/Sneakers/5.jpg", "Kvinner", "Joggesko", teller++);
            InsertNewProduct("Nike Free Run 3", 1200, 1,"Rosa", "Mesh", "Nike", "/Content/Images/Women/Sneakers/6.jpg", "Kvinner", "Joggesko", teller++);

            InsertNewProduct("Caribou Støvel", 1300, 1,"Grå", "Nylon", "Sorel", "/Content/Images/Women/Sneakers/1.jpg", "Kvinner", "Vintersko", teller++);
            InsertNewProduct("Vinterstøvel for Kvinner", 990,1, "Brun", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/2.jpg", "Kvinner", "Vintersko", teller++);
            InsertNewProduct("Vinterstøvel for Kvinner", 1250,1, "Hvit", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/3.jpg", "Kvinner", "Vintersko", teller++);
            InsertNewProduct("Classic Short", 2300, 1,"Brun", "Skinn", "Uggs", "/Content/Images/Women/Sneakers/4.jpg", "Kvinner", "Vintersko", teller++);
            InsertNewProduct("Vintersko for Kvinner", 790, 1,"Brun", "Skinn", "FriendSkorner", "/Content/Images/Women/Sneakers/5.jpg", "Kvinner", "Vintersko", teller++);
            
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