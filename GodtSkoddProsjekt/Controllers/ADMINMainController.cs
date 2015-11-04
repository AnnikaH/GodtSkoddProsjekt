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

        public ActionResult AdminCustomers()
        {
            // TODO: CHECK LOG IN

            return View();
        }

        public ActionResult AdminOrders()
        {
            // TODO: CHECK LOG IN

            return View();
        }

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
        public ActionResult UpdateAdminUser(int id)
        {
            return View();
        }
 
        // POST: ADMINMain/UpdateAdminUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAdminUser(int id, AdminUser adminUser)
        {
            var dal = new BusinessLogic();

            bool updateOk = dal.UpdateAdminUser(id, adminUser);

            if(updateOk)
                return RedirectToAction("AdminAdminUsers");

            return View();
        }

        /* GET: ADMINMain/DeleteAdminUser/5
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

        // --------------------------- AUTOGENERERT KODE: ----------------------------------

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
    }
}
