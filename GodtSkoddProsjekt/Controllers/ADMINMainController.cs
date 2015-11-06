﻿using System;
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
        private IBusinessLogic dbBLL;

        public ADMINMainController()
        {
            dbBLL = new BusinessLogic();
        }

        public ADMINMainController(IBusinessLogic stub)
        {
            dbBLL = stub;
        }

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
            // TODO: CHECK LOG IN (mer enn dette under?):

            if (Session["LoggedInAdmin"] != null && (bool)Session["LoggedInAdmin"]) // if logged in from before
                return RedirectToAction("Index");

            // else: Go to log in-page for administrators:

            return View();
        }

        public JsonResult CheckLogIn(String username, String password)
        {
            // checking login

            AdminUser adminUser = new AdminUser();
            adminUser.userName = username;
            adminUser.password = password;

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
            List<AdminUser> allAdminUsers = dbBLL.GetAdminUsers();

            return View(allAdminUsers);
            */
        }

        // --------------------------- AdminUser: -----------------------------------------

        public ActionResult AdminAdminUsers(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all AdminUsers (and buttons for deleting and updating them) + button to CreateAdminUser

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

        /* Called when searching for an AdminUser based on id:
        // GET: ADMINMain/GetAdminUser/5
        public ActionResult GetAdminUser(int id)
        {
            // TODO: CHECK LOG IN
            
            AdminUser adminUser = dbBLL.GetAdminUser(id);

            if (adminUser != null)
                return RedirectToAction("AdminAdminUsers", new { id = id });

            return RedirectToAction("AdminAdminUsers");
        }*/

        // Called from JavaScript-function when searching for a AdminUser based on id:
        // GET: ADMINMain/GetAdminUser/5
        public JsonResult GetAdminUser(int id)
        {
            // TODO: CHECK LOG IN
            
            AdminUser adminUser = dbBLL.GetAdminUser(id);

            if (adminUser != null)
                return Json(adminUser, JsonRequestBehavior.AllowGet);

            return Json("", JsonRequestBehavior.AllowGet);
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
            
            AdminUser adminUser = dbBLL.GetAdminUser(id);
            return View(adminUser);
        }
 
        // POST: ADMINMain/UpdateAdminUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminUser(int id, AdminUser adminUser)
        {
            // TODO: CHECK LOG IN
            
            bool updateOk = dbBLL.EditAdminUser(id, adminUser);

            if(updateOk)
                return RedirectToAction("AdminAdminUsers");

            return View();
        }

        public ActionResult DeleteAdminUser(int id)
        {
            // TODO: CHECK LOG IN
            
            bool deleteOk = dbBLL.DeleteAdminUser(id);

            //if (deleteOk)

            return RedirectToAction("AdminAdminUsers");
        }

        public ActionResult CancelAdminUser()
        {
            // TODO: CHECK LOG IN

            return RedirectToAction("AdminAdminUsers");
        }

        // ---------------------------------- User ------------------------------

        public ActionResult AdminCustomers(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all Users (and buttons for deleting and updating them) + button to CreateUser
            
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

        /* Called when searching for a User based on id:
        // GET: ADMINMain/GetUser/5
        public ActionResult GetUser(int id)
        {
            // TODO: CHECK LOG IN
            
            User user = dbBLL.GetUser(id);

            if (user != null)
                return RedirectToAction("AdminCustomers", new { id = id });

            return RedirectToAction("AdminCustomers");
        }*/

        // Called from JavaScript-function when searching for a User based on id:
        // GET: ADMINMain/GetUser/5
        public JsonResult GetUser(int id)
        {
            // TODO: CHECK LOG IN
            
            User user = dbBLL.GetUser(id);

            if (user != null)
                return Json(user, JsonRequestBehavior.AllowGet);

            return Json("", JsonRequestBehavior.AllowGet);
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
                bool changeOK = dbBLL.EditUser(id, user);

                if (changeOK)
                    return RedirectToAction("AdminCustomers");
            }

            return View();
        }

        public ActionResult DeleteUser(int id)
        {
            // TODO: CHECK LOG IN
            
            bool deleteOk = dbBLL.DeleteUser(id);

            //if (deleteOk)
            
            return RedirectToAction("AdminCustomers");
        }

        public ActionResult CancelUser()
        {
            // TODO: CHECK LOG IN

            return RedirectToAction("AdminCustomers");
        }

        // ----------------------------------- Product -------------------------------------

        public ActionResult AdminProducts(int? id)
        {
            // TODO: CHECK LOG IN

            // Showing all Products (and buttons for deleting and updating them) + button to CreateProduct
            
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

        /* Called when searching for a Product based on id:
        // GET: ADMINMain/GetProduct/5
        public ActionResult GetProduct(int id)
        {
            // TODO: CHECK LOG IN
            
            Product product = dbBLL.GetProduct(id);

            if (product != null)
                return RedirectToAction("AdminProducts", new { id = id });

            return RedirectToAction("AdminProducts");
        }*/

        // Called from JavaScript-function when searching for a Product based on id:
        // GET: ADMINMain/GetProduct/5
        public JsonResult GetProduct(int id)
        {
            // TODO: CHECK LOG IN
            
            Product product = dbBLL.GetProduct(id);

            if (product != null)
                return Json(product, JsonRequestBehavior.AllowGet);

            return Json("", JsonRequestBehavior.AllowGet);
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
                bool changeOK = dbBLL.EditProduct(id, product);

                if (changeOK)
                    return RedirectToAction("AdminProducts");
            }

            return View();
        }

        public ActionResult DeleteProduct(int id)
        {
            // TODO: CHECK LOG IN
            
            bool deleteOk = dbBLL.DeleteProduct(id);

            //if (deleteOk)

            return RedirectToAction("AdminProducts");
        }

        public ActionResult CancelProduct()
        {
            // TODO: CHECK LOG IN

            return RedirectToAction("AdminProducts");
        }

        // ------------------------------- Order og Orderline ----------------------------------

        public ActionResult AdminOrders(int id)    // id is User ID and must be sent in
        {
            // TODO: CHECK LOG IN

            // Showing all Orders for specified User (id) (and buttons for deleting and updating them) + button to CreateOrder
            
            /* Must store this userId in a session-variable so that can check if user has
            an order with the searched for orderId: */
            Session["UserIdForOrders"] = id;

            List<Order> orders = new List<Order>();

            Order order = (Order)Session["Order"];

            if (order != null)
            {
                orders.Add(order);
                /* If Session["Order"] contains an Order, then only show this order
                (admin has searched/called GetOrder(orderId))*/
                Session["Order"] = null;    // reset
            }
            else
            {
                orders = dbBLL.GetOrders(id);  // GetOrders from User ID (all orders for this user)
            }

            return View(orders);
        }

        /* Called when searching for an Order based on id:
        // GET: ADMINMain/GetOrder/5
        public ActionResult GetOrder(int id)   // must check if this orderId belongs to the user
        {
            // TODO: CHECK LOG IN
            
            Order order = dbBLL.GetOrder(id);

            int userId = (int)Session["UserIdForOrders"];    // Gets stored in session-variable in AdminOrders

            if (order != null && userId == order.userID)
            {
                Session["Order"] = order;
            }
            else
            {
                Session["Order"] = null;
            }

            return RedirectToAction("AdminOrders", new { id = userId });
        }*/

        // Called from JavaScript-function when searching for an Order based on id:
        // GET: ADMINMain/GetOrder/5
        public JsonResult GetOrder(int id)
        {
            // TODO: CHECK LOG IN
            
            Order order = dbBLL.GetOrder(id);

            int userId = (int)Session["UserIdForOrders"];    // Gets stored in session-variable in AdminOrders

            if (order != null && userId == order.userID)
            {
                Session["Order"] = order;
                return Json(order, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["Order"] = null;
            }

            return Json("", JsonRequestBehavior.AllowGet);
            //return RedirectToAction("AdminOrders", new { id = userId });
        }

        // GET: ADMINMain/CreateOrder
        public ActionResult CreateOrder()
        {
            // TODO: CHECK LOG IN

            return View();
        }

        // POST: ADMINMain/CreateOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(Order order)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                bool insertOK = dbBLL.CreateOrder(order);

                if (insertOK)
                    return RedirectToAction("AdminOrders", new { id = order.userID });
            }

            return View();
        }

        // GET: ADMINMain/EditOrder/5
        public ActionResult EditOrder(int id)
        {
            // TODO: CHECK LOG IN
            
            Order order = dbBLL.GetOrder(id);
            return View(order);
        }

        // POST: ADMINMain/EditOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(int id, Order order)
        {
            // TODO: CHECK LOG IN

            if (ModelState.IsValid)
            {
                bool changeOK = dbBLL.EditOrder(id, order);

                if (changeOK)
                    return RedirectToAction("AdminOrders", new { id = order.userID });
            }

            return View();
        }

        public ActionResult DeleteOrder(int id)
        {
            // TODO: CHECK LOG IN
            
            bool deleteOk = dbBLL.DeleteOrder(id);

            //if (deleteOk)

            int userId = (int)Session["UserIdForOrders"];    // Gets stored in session-variable in AdminOrders

            return RedirectToAction("AdminOrders", new { id = userId });
        }

        public ActionResult CancelOrder()
        {
            // TODO: CHECK LOG IN

            int userId = (int)Session["UserIdForOrders"];    // Gets stored in session-variable in AdminOrders

            return RedirectToAction("AdminOrders", new { id = userId });
        }

        // Legge inn Create, Edit, Delete for Orderline også?
        // AJAX-kall til DeleteOrderline(int id) i så fall. Får inn Orderline-id
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