using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GodtSkoddProsjekt.Models;
using BLL;  // just for added code in Index(int? id) (filling database with products)

namespace GodtSkoddProsjekt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? id)
        {
            // Fill database with pre-defined products (if none exists):
            var dbBLL = new BusinessLogic();

            if (!dbBLL.DatabaseProductsFilled())
                dbBLL.CreateDatabaseContent();

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

            // Checking Cart
            if(Session["Cart"] == null)
            {
                Order Cart = new Order();
                Cart.date = DateTime.Now;
                Cart.orderlines = new List<Orderline>();
                Session["Cart"] = ViewBag.Cart = Cart;
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
            // checking login

            LoginUser loginUser = new LoginUser();
            loginUser.userName = username;
            loginUser.password = password;

            var dbGodtSkodd = new DBGodtSkodd();
            var userId = dbGodtSkodd.GetUserIdInDB(loginUser);

            //if (dbGodtSkodd.UserInDb(loginUser))
            if (userId != -1)
            {
                // yes username and password is OK
                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;

                Session["UserId"] = userId;

                JsonResult jsonOutput = Json(loginUser, JsonRequestBehavior.AllowGet);
                return jsonOutput;
            }
            else
            {
                // no
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                return null;
            }
        }

        public ActionResult LogOut()
        {
            Session["LoggedIn"] = false;
            ViewBag.LoggedIn = false;
            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        /*public ActionResult Details(int id)
        {
            var dbGodtSkodd = new DBGodtSkodd();
            Product product = dbGodtSkodd.GetProduct(id);
            return View(product);
        }*/

        public void AddToCart(int id, int quantity)
        {
            Orderline newOrderLine = new Orderline();
            newOrderLine.productId = id;
            newOrderLine.quantity = quantity;
            Order Cart = (Order) Session["Cart"];
            Cart.orderlines.Add(newOrderLine);
            Session["Cart"] = Cart;
        }

        public void Buy(Order order)
        {
            Order Cart = (Order) Session["Cart"];
            Cart.userID = 1; //hente bruker fra session og legge inn ID til user i Cart før den sendes videre
            DBGodtSkodd db = new DBGodtSkodd();
            db.CreateOrder(Cart);
        }
    }
}