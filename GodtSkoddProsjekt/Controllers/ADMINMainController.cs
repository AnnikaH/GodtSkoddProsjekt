using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace GodtSkoddProsjekt.Controllers
{
    public class ADMINMainController : Controller
    {
        //---------------------- TOR SIN KODE FRA TIMEN (Lagdeling): ------------------------//
        /*
        public ActionResult Liste()
        {
            var kundeDb = new KundeLogikk();
            List<Kunde> alleKunder = kundeDb.hentAlle();
            return View(alleKunder);
        }

        public ActionResult Detaljer(int id)
        {
            var kundeDb = new KundeLogikk();
            Kunde enKunde = kundeDb.hentEnKunde(id);
            return View(enKunde);
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrer(Kunde innKunde)
        {
            if (ModelState.IsValid)
            {
                var kundeDb = new KundeLogikk();
                bool insertOK = kundeDb.settInn(innKunde);
                if (insertOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

        public ActionResult Endre(int id)
        {
            var kundeDb = new KundeLogikk();
            Kunde enKunde = kundeDb.hentEnKunde(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult Endre(int id, Kunde endreKunde)
        {

            if (ModelState.IsValid)
            {
                var kundeDb = new KundeLogikk();
                bool endringOK = kundeDb.endreKunde(id, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("Liste");
                }
            }
            return View();
        }

        public ActionResult Slett(int id)
        {
            var kundeDb = new KundeLogikk();
            Kunde enKunde = kundeDb.hentEnKunde(id);
            return View(enKunde);
        }

        [HttpPost]
        public ActionResult Slett(int id, Kunde slettKunde)
        {
            var kundeDb = new KundeLogikk();
            bool slettOK = kundeDb.slettKunde(id);
            if(slettOK)
            {
                return RedirectToAction("Liste");
            }
            return View();
        }
        */

        // --------------------------- Log in/out ------------------------------

        public ActionResult LogIn()
        {
            // Log in-page for administrators

            return View();
        }

        public JsonResult CheckLogIn(String username, String password)
        {
            // checking login

            AdminUser adminUser = new AdminUser();
            adminUser.userName = username;
            adminUser.password = password;

            var dbBLL = new BusinessLogic();
            var adminId = dbBLL.GetAdminIdInDB(adminUser);

            if (adminId != -1)
            {
                // yes username and password is OK
                Session["LoggedInAdmin"] = true;
                ViewBag.LoggedInAdmin = true;

                Session["AdminId"] = adminId;

                JsonResult jsonOutput = Json(adminUser, JsonRequestBehavior.AllowGet);
                return jsonOutput;
            }
            else
            {
                // no
                Session["LoggedInAdmin"] = false;
                ViewBag.LoggedInAdmin = false;
                return null;
            }
        }

        public ActionResult LogOut()
        {
            Session["LoggedInAdmin"] = false;
            ViewBag.LoggedInAdmin = false;
            return RedirectToAction("Index", "Home");
        }

        // ------------------------------- Index ---------------------------------

        public ActionResult Index()
        {
            // Main page for administrators (you get here after logged in successfully)

            // Checking login:
            if (Session["LoggedInAdmin"] == null)
            {
                // da definerer vi den og setter den til false
                Session["LoggedInAdmin"] = false;
                ViewBag.LoggedInAdmin = false; // oppdaterer denne også!
            }
            else
            {
                // vil så hente ut statusen til session'en og legge denne over i ViewBag'en:
                ViewBag.LoggedInAdmin = (bool)Session["LoggedInAdmin"]; // Husk: Må castes!
            }

            return View();

            /*
            var dbBLL = new BusinessLogic();
            List<AdminUser> allAdminUsers = dbBLL.GetAdminUsers();

            return View(allAdminUsers);
            */
        }

        // --------------------------- AdminUser: -----------------------------------------

        public ActionResult AdminAdminUsers(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all AdminUsers (and buttons for deleting and updating them) + button to CreateAdminUser

            var dbBLL = new BusinessLogic();

            List<AdminUser> adminUsers = new List<AdminUser>();

            if (id.HasValue)
            {
                int adminId = (int)id;

                AdminUser adminUser = dbBLL.GetAdminUser(adminId);

                if (adminUser != null)
                {
                    adminUsers.Add(adminUser);
                    return View(adminUsers);
                }
            }
            
            adminUsers = dbBLL.GetAdminUsers();
            
            return View(adminUsers);
        }

        // Called when searching for an AdminUser based on id:
        // GET: ADMINMain/GetAdminUser/5
        public ActionResult GetAdminUser(int adminId)
        {
            var dbBLL = new BusinessLogic();
            AdminUser adminUser = dbBLL.GetAdminUser(adminId);

            if (adminUser != null)
                return RedirectToAction("AdminAdminUsers", new { id = adminId });

            return RedirectToAction("AdminAdminUsers");
        }

        // GET: ADMINMain/CreateAdminUser
        public ActionResult CreateAdminUser()
        {
            // TODO: CHECK LOG IN

            return View();
        }

        // POST: ADMINMain/CreateAdminUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminUser(AdminUser adminUser)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                var dal = new BusinessLogic();
                bool insertOK = dal.CreateAdminUser(adminUser);

                if (insertOK)
                {
                    Session["LoggedInAdmin"] = true;
                    ViewBag.LoggedInAdmin = true;
                    
                    Session["AdminId"] = dal.GetAdminIdInDB(adminUser);

                    return RedirectToAction("AdminAdminUsers");
                }
            }

            return View();
        }
        
        // GET: ADMINMain/UpdateAdminUser/5
        public ActionResult EditAdminUser(int id)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();
            AdminUser adminUser = dbBLL.GetAdminUser(id);
            return View(adminUser);
        }
 
        // POST: ADMINMain/UpdateAdminUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminUser(int id, AdminUser adminUser)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();

            bool updateOk = dbBLL.EditAdminUser(id, adminUser);

            if(updateOk)
                return RedirectToAction("AdminAdminUsers");

            return View();
        }

        /* Vil ikke gå til ny side for å slette - vil bare oppdatere AdminAdminUsers-siden (ajax-kall)
        // GET: ADMINMain/DeleteAdminUser/5
        public ActionResult DeleteAdminUser(int id)
        {
            return View();
        }*/

        // Called from JavaScript (AJAX) (when clicking delete-button):
        public JsonResult DeleteAdminUser(int id)
        {
            var dbBLL = new BusinessLogic();

            bool deleteOk = dbBLL.DeleteAdminUser(id);

            JsonResult jsonOutput;

            if (deleteOk)
                jsonOutput = Json(true, JsonRequestBehavior.AllowGet);
            else
                jsonOutput = Json(false, JsonRequestBehavior.AllowGet);

            return jsonOutput;
        }

        // ---------------------------------- User ------------------------------

        public ActionResult AdminCustomers(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all Users (and buttons for deleting and updating them) + button to CreateUser

            var dbBLL = new BusinessLogic();

            List<User> users = new List<User>();

            if (id.HasValue)
            {
                int userId = (int)id;

                User user = dbBLL.GetUser(userId);

                if (user != null)
                {
                    users.Add(user);
                    return View(users);
                }
            }

            users = dbBLL.GetUsers();

            return View(users);
        }

        // Called when searching for a User based on id:
        // GET: ADMINMain/GetUser/5
        public ActionResult GetUser(int userId)
        {
            var dbBLL = new BusinessLogic();
            User user = dbBLL.GetUser(userId);

            if (user != null)
                return RedirectToAction("AdminCustomers", new { id = userId });

            return RedirectToAction("AdminAdminUsers");
        }

        // GET: ADMINMain/CreateUser
        public ActionResult CreateUser()
        {
            // TODO: CHECK LOG IN

            return View();
        }

        // POST: ADMINMain/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                var dbBLL = new BusinessLogic();
                bool insertOK = dbBLL.CreateUser(user);

                if (insertOK)
                    return RedirectToAction("AdminCustomers");
            }

            return View();
        }

        // GET: ADMINMain/EditUser/5
        public ActionResult EditUser(int id)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();
            User user = dbBLL.GetUser(id);
            return View(user);
        }

        // POST: ADMINMain/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int id, User user)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                var dbBLL = new BusinessLogic();
                bool changeOK = dbBLL.EditUser(id, user);

                if (changeOK)
                    return RedirectToAction("AdminCustomers");
            }

            return View();
        }

        /* Vil ikke gå til ny side for å slette - vil bare oppdatere AdminCustomers-siden (ajax-kall)
        // GET: ADMINMain/DeleteAdminUser/5
        public ActionResult DeleteAdminUser(int id)
        {
            return View();
        }*/

        // Called from JavaScript (AJAX) (when clicking delete-button):
        public JsonResult DeleteUser(int id)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();
            bool deleteOk = dbBLL.DeleteUser(id);

            JsonResult jsonOutput;

            if (deleteOk)
                jsonOutput = Json(true, JsonRequestBehavior.AllowGet);
            else
                jsonOutput = Json(false, JsonRequestBehavior.AllowGet);

            return jsonOutput;
        }

        // ----------------------------------- Product -------------------------------------

        public ActionResult AdminProducts(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all Products (and buttons for deleting and updating them) + button to CreateProduct

            var dbBLL = new BusinessLogic();

            List<Product> products = new List<Product>();

            if (id.HasValue)
            {
                int productId = (int)id;

                Product product = dbBLL.GetProduct(productId);

                if (product != null)
                {
                    products.Add(product);
                    return View(products);
                }
            }

            products = dbBLL.GetProducts();

            return View(products);
        }

        // Called when searching for a Product based on id:
        // GET: ADMINMain/GetProduct/5
        public ActionResult GetProduct(int productId)
        {
            var dbBLL = new BusinessLogic();
            Product product = dbBLL.GetProduct(productId);

            if (product != null)
                return RedirectToAction("AdminProducts", new { id = productId });

            return RedirectToAction("AdminProducts");
        }

        // GET: ADMINMain/CreateProduct
        public ActionResult CreateProduct()
        {
            // TODO: CHECK LOG IN

            return View();
        }

        // POST: ADMINMain/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                var dbBLL = new BusinessLogic();
                bool insertOK = dbBLL.CreateProduct(product);

                if (insertOK)
                    return RedirectToAction("AdminProducts");
            }

            return View();
        }

        // GET: ADMINMain/EditProduct/5
        public ActionResult EditProduct(int id)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();
            Product product = dbBLL.GetProduct(id);
            return View(product);
        }

        // POST: ADMINMain/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id, Product product)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                var dbBLL = new BusinessLogic();
                bool changeOK = dbBLL.EditProduct(id, product);

                if (changeOK)
                    return RedirectToAction("AdminProducts");
            }

            return View();
        }

        // Called from JavaScript (AJAX) (when clicking delete-button):
        public JsonResult DeleteProduct(int id)
        {
            // TODO: CHECK LOG IN

            var dbBLL = new BusinessLogic();
            bool deleteOk = dbBLL.DeleteProduct(id);

            JsonResult jsonOutput;

            if (deleteOk)
                jsonOutput = Json(true, JsonRequestBehavior.AllowGet);
            else
                jsonOutput = Json(false, JsonRequestBehavior.AllowGet);

            return jsonOutput;
        }

        // ------------------------------- Order og Orderline ----------------------------------

        public ActionResult AdminOrders()
        {
            // TODO: CHECK LOG IN

            return View();
        }
    }

    /* --------------------------- AUTOGENERERT KODE: ----------------------------------

    // GET: ADMINMain/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

        // GET: ADMINMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMINMain/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ADMINMain/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ADMINMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ADMINMain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ADMINMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }*/
}