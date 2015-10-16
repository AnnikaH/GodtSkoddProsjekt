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

        public void CreateAllProducts()
        {
            DBGodtSkodd dbGodtSkodd = new DBGodtSkodd();

            dbGodtSkodd.CreateProduct(CreateNewProduct("Traktor regnstøvler", 200, 1, "Rød", "Gummi", "Amazon", "/Content/Images/Boys/Boots/1.jpg", "Boys", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Frosk regnstøvler", 290, 1, "Grønn", "Gummi", "Amazon", "/Content/Images/Boys/Boots/2.jpg", "Boys", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Viking regnstøvler", 230, 1, "Blå", "Gummi", "Viking", "/Content/Images/Boys/Boots/3.jpg", "Boys", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Hunter regnstøvler", 800, 1, "Blå/Grå", "Gummi", "Hunter", "/Content/Images/Boys/Boots/4.jpg", "Boys", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Byggmester Bob", 450, 1, "Blå", "Gummi", "BobBuilder", "/Content/Images/Boys/Boots/5.jpg", "Boys", "Boots"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 370, 1,"Hvit/Svart", "Skinn", "AliExpress", "/Content/Images/Boys/DressShoes/1.jpg", "Boys", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 350, 1,"Svart/Sølv", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/2.jpg", "Boys", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 500, 1,"Svart", "Skinn", "LM83", "/Content/Images/Boys/DressShoes/3.jpg", "Boys", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 500, 1,"Svart", "Skinn", "Japanned", "/Content/Images/Boys/DressShoes/4.jpg", "Boys", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 390, 1,"Svart", "Skinn", "ToysRUs", "/Content/Images/Boys/DressShoes/5.jpg", "Boys", "DressShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Sommersandaler", 390,1, "Hvit", "Mesh", "Uovo", "/Content/Images/Boys/Sandals/1.jpg", "Boys", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Baby gåsko", 550, 1,"Hvit/Blå", "Skinn", "Caroch", "/Content/Images/Boys/Sandals/2.jpg", "Boys", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Mikke Mus", 150, 1,"Blå", "Mesh", "Disney", "/Content/Images/Boys/Sandals/3.jpg", "Boys", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Relix sandaler", 590, 1,"Svart", "Mesh", "Skechers", "/Content/Images/Boys/Sandals/4.jpg", "Boys", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Coga Sport", 350, 1,"Blå", "Mesh", "Coga", "/Content/Images/Boys/Sandals/5.jpg", "Boys", "Sandals"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Joggesko", 350, 1,"Blå", "Mesh", "Jtengda", "/Content/Images/Boys/Sneakers/1.jpg", "Boys", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Air Cushion", 390, 1,"Blå", "Mesh", "KanlBear", "/Content/Images/Boys/Sneakers/2.jpg", "Boys", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Totoho Lite", 300, 1,"Svart", "Mesh", "Totoho", "/Content/Images/Boys/Sneakers/3.jpg", "Boys", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Y-8 joggesko", 490, 1,"Svart", "Mesh", "Miqixz", "/Content/Images/Boys/Sneakers/4.jpg", "Boys", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Panther fotballsko", 500, 1,"Grønn/Gul", "Mesh", "TieBao", "/Content/Images/Boys/Sneakers/5.jpg", "Boys", "Sneakers"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 450, 1, "Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/1.jpg", "Boys", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 500, 1, "Svart", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/2.jpg", "Boys", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("PU skinnstøvler", 130, 1,"Grønn", "Skinn", "AliExpress", "/Content/Images/Boys/WinterShoes/3.jpg", "Boys", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Hongda CowHide", 380, 1,"Grønn", "Skinn", "Hongda", "/Content/Images/Boys/WinterShoes/4.jpg", "Boys", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vinter utesko", 150, 1,"Grønn", "Gummi", "AliExpress", "/Content/Images/Boys/WinterShoes/5.jpg", "Boys", "WinterShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Dora støvler", 500, 1,"Rosa", "Gummi", "Dora The Explorer", "/Content/Images/Girls/Boots/1.jpg", "Girls", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Minni Mus", 390, 1,"Rød", "Gummi", "Disney", "/Content/Images/Girls/Boots/2.jpg", "Girls", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Frozen", 500, 1,"Grønn", "Gummi", "Disney", "/Content/Images/Girls/Boots/3.jpg", "Girls", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Kanin", 290, 1,"Rosa", "Gummi", "LostLands", "/Content/Images/Girls/Boots/4.jpg", "Girls", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Hello Kitty", 450, 1,"Rosa", "Gummi", "Hello Kitty", "/Content/Images/Girls/Boots/5.jpg", "Girls", "Boots"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Formelle sko", 600, 1,"Rosa", "Skinn", "The Bay", "/Content/Images/Girls/DressShoes/1.jpg", "Girls", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 150, 1, "Gull", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/2.jpg", "Girls", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersko", 590,1, "Hvit", "Skinn", "LM83", "/Content/Images/Girls/DressShoes/3.jpg", "Girls", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Baby pensko", 100, 1, "Sølv", "Syntetisk Skinn", "LM83", "/Content/Images/Girls/DressShoes/4.jpg", "Girls", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 450, 1,"Svart", "Skinn", "JoyLand", "/Content/Images/Girls/DressShoes/5.jpg", "Girls", "DressShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersandaler", 390,1, "Rød", "Skinn", "The Bay", "/Content/Images/Girls/Sandals/1.jpg", "Girls", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Blomstersandaler", 150, 1,"Hvit", "Syntetisk Skinn", "Monsoon", "/Content/Images/Girls/Sandals/2.jpg", "Girls", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Princess BowTie", 450, 1,"Rosa", "Skinn", "DCamp", "/Content/Images/Girls/Sandals/3.jpg", "Girls", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Barnesandaler", 550, 1,"Grå", "Mesh", "Gzzhl", "/Content/Images/Girls/Sandals/4.jpg", "Girls", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Jewelled sandaler", 290, 1,"Rosa", "Syntetisk Skinn", "AliCDN", "/Content/Images/Girls/Sandals/5.jpg", "Girls", "Sandals"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Dansesko", 500, 1,"Lilla", "Mesh", "Uovo", "/Content/Images/Girls/Sneakers/1.jpg", "Girls", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Originals ZX", 700,1, "Lilla", "Mesh", "Adidas", "/Content/Images/Girls/Sneakers/2.jpg", "Girls", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Eclipse løpesko", 490, 1,"Rosa", "Mesh", "Eclipse", "/Content/Images/Girls/Sneakers/3.jpg", "Girls", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Outdoor Sport", 250, 1,"Rosa", "Mesh", "AliExpress", "/Content/Images/Girls/Sneakers/4.jpg", "Girls", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("GT 1000-4", 550, 1,"Rosa", "Mesh", "Asics", "/Content/Images/Girls/Sneakers/5.jpg", "Girls", "Sneakers"));
            
            dbGodtSkodd.CreateProduct(CreateNewProduct("Lobbing Ball", 100,1, "Rosa", "Plush", "AliExpress", "/Content/Images/Girls/WinterShoes/1.jpg", "Girls", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Candy Thermal", 300, 1,"Lilla", "Plush", "Coga", "/Content/Images/Girls/WinterShoes/2.jpg", "Girls", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 450, 1,"Rosa", "Buffalo Hide", "AliExpress", "/Content/Images/Girls/WinterShoes/3.jpg", "Girls", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Rabbit Bowknot", 250, 1,"Rosa", "Skinn", "AliExpress", "/Content/Images/Girls/WinterShoes/4.jpg", "Girls", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Juicy Couture", 2000, 1,"Rosa", "Velour", "Juicy", "/Content/Images/Girls/WinterShoes/5.jpg", "Girls", "WinterShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Stål-Shank", 200, 1,"Svart", "Gummi", "Walmart", "/Content/Images/Men/Boots/1.jpg", "Men", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Male Martin", 390, 1,"Svart", "Gummi", "Martin", "/Content/Images/Men/Boots/2.jpg", "Men", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Tretorn Strala", 600, 1,"Svart", "Gummi", "Tretorn", "/Content/Images/Men/Boots/3.jpg", "Men", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Economy Plain", 180,1, "Svart", "Gummi", "Tingley", "/Content/Images/Men/Boots/4.jpg", "Men", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Hot Designers", 390, 1,"Svart", "Gummi", "Warrior", "/Content/Images/Men/Boots/5.jpg", "Men", "Boots"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Bata", 8990, 1,"Svart", "Skinn", "Bata", "/Content/Images/Men/DressShoes/1.jpg", "Men", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Sir Corbett", 6000, 1,"Svart", "Syntetisk Skinn", "Sir Corbett", "/Content/Images/Men/DressShoes/2.jpg", "Men", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Fellini", 3249, 1,"Brun", "Skinn", "Fellini", "/Content/Images/Men/DressShoes/3.jpg", "Men", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Michael Toschi", 5950, 1,"Svart", "Leather", "Michael Toschi", "/Content/Images/Men/DressShoes/4.jpg", "Men", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Roxxii", 8990, 1,"Svart", "Faux Patent Skinn", "Roxxii", "/Content/Images/Men/DressShoes/5.jpg", "Men", "DressShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Orvis", 990, 1,"Brun", "Skinn", "Orvis", "/Content/Images/Men/Sandals/1.jpg", "Men", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Sandaler", 859, 1,"Brun", "Faux Skinn", "Bacca Bucci", "/Content/Images/Men/Sandals/2.jpg", "Men", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Docker Marin", 750, 1,"Brun", "Skinn", "Docker Marin", "/Content/Images/Men/Sandals/3.jpg", "Men", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Supreme Equipt", 300, 1,"Svart", "Mesh", "Sketchers", "/Content/Images/Men/Sandals/4.jpg", "Men", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Grønne sandaler", 1500, 1,"Grønn", "Faux Skinn", "Weinbrenner", "/Content/Images/Men/Sandals/5.jpg", "Men", "Sandals"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("TravelWalker", 650,1, "Black", "Mesh", "Kroten", "/Content/Images/Men/Sneakers/1.jpg", "Men", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("GT 1000", 990,1, "Svart/Grønn", "Mesh", "Asics", "/Content/Images/Men/Sneakers/2.jpg", "Men", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Trail", 790,1, "Grå", "Mesh", "New Balance", "/Content/Images/Men/Sneakers/3.jpg", "Men", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Track", 990, 1,"Svart", "Mesh", "Adidas", "/Content/Images/Men/Sneakers/4.jpg", "Men", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Atletisk sko", 500, 1,"Hvit", "Mesh", "Man", "/Content/Images/Men/Sneakers/5.jpg", "Men", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("3.0 V5", 890, 1,"Blå", "Mesh", "Nike", "/Content/Images/Men/Sneakers/6.jpg", "Men", "Sneakers"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Erkek Boots", 2490, 1,"Brun", "Skinn", "Erkek", "/Content/Images/Men/WinterShoes/1.jpg", "Men", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Caribou Boots", 1300, 1,"Svart", "Nylon", "Sorel", "/Content/Images/Men/WinterShoes/2.jpg", "Men", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Classic Vintage", 950, 1,"Brun", "Skinn", "Doozoo", "/Content/Images/Men/WinterShoes/3.jpg", "Men", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Jobbsko", 400, 1,"Brun", "Cow Split", "AliExpress", "/Content/Images/Men/WinterShoes/4.jpg", "Men", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Shearling", 1050, 1,"Brun", "Skinn", "LL Bean", "/Content/Images/Men/WinterShoes/5.jpg", "Men", "WinterShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Regnstøvler", 790, 1,"Svart", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/1.jpg", "Women", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Ownshoe", 500, 1,"Svart/Hvit", "Gummi", "OwnShoe", "/Content/Images/Women/Boots/2.jpg", "Women", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Salvatore Ferragamo", 3600,1, "Svart", "Gummi", "Salvatore Ferragamo", "/Content/Images/Women/Boots/3.jpg", "Women", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Hunter støvler", 1250,1, "Svart", "Gummi", "Hunter", "/Content/Images/Women/Boots/4.jpg", "Women", "Boots"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Dasko Vail", 1150,1, "Blå", "Gummi", "Dasko Vail", "/Content/Images/Women/Boots/5.jpg", "Women", "Boots"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Rebeca", 8490, 1,"Blå", "Suede", "Jessica Simpson", "/Content/Images/Women/DressShoes/1.jpg", "Women", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Ruched saueskinn", 980,1, "Svart", "Saueskinn", "Ruched", "/Content/Images/Women/DressShoes/2.jpg", "Women", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Quirkin", 500,1, "Hvit", "Syntetisk Skinn", "Quirkin", "/Content/Images/Women/DressShoes/3.jpg", "Women", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsko", 750, 1,"Svart", "Suede", "Europe American Express", "/Content/Images/Women/DressShoes/4.jpg", "Women", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsandaler", 1100, 1,"Svart", "Suede", "Traditionals", "/Content/Images/Women/DressShoes/5.jpg", "Women", "DressShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Pensko", 1500,1, "Rosa", "Suede", "WUFashion", "/Content/Images/Women/DressShoes/6.jpg", "Women", "DressShoes"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Plattformsandaler", 850, 1,"Brun", "Skinn", "Laurent", "/Content/Images/Women/Sandals/1.jpg", "Women", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Åpne sandaler", 200, 1,"Hvit", "Syntetisk Skinn", "Ahuh", "/Content/Images/Women/Sandals/2.jpg", "Women", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Broderte sandaler", 250,1, "Rød", "Vev", "Polyvore", "/Content/Images/Women/Sandals/3.jpg", "Women", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Gladiatorsandaler", 300, 1,"Brun", "Syntetisk Skinn", "The Bay", "/Content/Images/Women/Sandals/4.jpg", "Women", "Sandals"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Rungsted sandaler", 1000, 1,"Svart", "Skinn", "Ecco", "/Content/Images/Women/Sandals/5.jpg", "Women", "Sandals"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Atletisk sko", 350, 1,"Blå/Rosa", "Mesh", "Skechers", "/Content/Images/Women/Sneakers/1.jpg", "Women", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Gel Kayano 18", 950, 1,"Svart", "Mesh", "Asics", "/Content/Images/Women/Sneakers/2.jpg", "Women", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Puma", 690, 1,"Grå", "Mesh", "Puma", "/Content/Images/Women/Sneakers/3.jpg", "Women", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Riaze", 790, 1,"Svart", "Mesh", "Puma", "/Content/Images/Women/Sneakers/4.jpg", "Women", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("XR Mission", 900,1, "Blå", "Mesh", "Salomon", "/Content/Images/Women/Sneakers/5.jpg", "Women", "Sneakers"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Nike Free Run", 1200, 1,"Rosa", "Mesh", "Nike", "/Content/Images/Women/Sneakers/6.jpg", "Women", "Sneakers"));

            dbGodtSkodd.CreateProduct(CreateNewProduct("Caribou", 1300, 1,"Grå", "Nylon", "Sorel", "/Content/Images/Women/Sneakers/1.jpg", "Women", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 990,1, "Brun", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/2.jpg", "Women", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vinterstøvler", 1250,1, "Hvit", "Skinn", "Fashionells", "/Content/Images/Women/Sneakers/3.jpg", "Women", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Classic Short", 2300, 1,"Brun", "Skinn", "Uggs", "/Content/Images/Women/Sneakers/4.jpg", "Women", "WinterShoes"));
            dbGodtSkodd.CreateProduct(CreateNewProduct("Vintersko", 790, 1,"Brun", "Skinn", "FriendSkorner", "/Content/Images/Women/Sneakers/5.jpg", "Women", "WinterShoes"));

            RedirectToAction("Index");
        }

        private Product CreateNewProduct(String n, double p, int s, String c, String m, String b, String u, String g, String t)
        {
            Product product = new Product();

            product.name = n;
            product.price = p;
            product.size = s;
            product.color = c;
            product.material = m;
            product.brand = b;
            product.url = u;
            product.gender = g;
            product.type = t;

            return product;
        }
    }
}