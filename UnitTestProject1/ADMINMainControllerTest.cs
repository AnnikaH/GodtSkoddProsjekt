using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using GodtSkoddProsjekt.Controllers;
using Model;
using BLL;
using DAL;
using MvcContrib.TestHelper;


namespace UnitTestProject1
{
    [TestClass]
    public class ADMINMainControllerTest
    {
        /*

        Trenger flere enn én test-metode per metode i ADMINMainController, men dette er en start
        
        */
        
        // ----------------------------- Log in/out --------------------------

        // Tester for å sjekke LogIn():

        [TestMethod]
        public void LogIn()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController();
            SessionMock.InitializeController(controller);
            
            controller.Session["LoggedInAdmin"] = false;
            // Act
            var result = (ViewResult)controller.LogIn();
            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

    [TestMethod]
        public void Login_NotOk()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            
            var actionResult = (RedirectToRouteResult)controller.LogIn();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Index");
        }

        // Tester for å sjekke LoggedIn():

        [TestMethod]
        public void LoggedIn_null()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = null;
            // Act

            var actionResult = (bool)controller.LoggedIn();

            // Assert
            Assert.IsFalse(actionResult);

        }

        [TestMethod]
        public void LoggedIn_True()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (bool)controller.LoggedIn();

            // Assert
            Assert.IsTrue(actionResult);

        }

        [TestMethod]
        public void LoggedIn_False()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController();
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = false;
            // Act

            var actionResult = (bool)controller.LoggedIn();

            // Assert
            Assert.IsFalse(actionResult);

        }

        // Tester for å sjekke CheckLogIn(String username, String password):

        [TestMethod]
        public void CheckLogIn_null_existing_user()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            var userName = "Test";
            var password = "Testing1";
            // Act

            var actionResult = (JsonResult)controller.CheckLogIn(userName, password);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
        }

        [TestMethod]
        public void CheckLogIn_Json()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            var userName = "";
            var password = "";
            // Act

            var actionResult = (JsonResult)controller.CheckLogIn(userName, password);
            // Assert
            Assert.IsNull(actionResult);
        }

        // Tester for å sjekke LogOut():

        [TestMethod]
        public void LogOut()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            // Act

            var actionResult = (RedirectToRouteResult)controller.LogOut();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Index");

        }

        // --------------------------- Index --------------------------

        // Tester for å sjekke Index():

        [TestMethod]
        public void Index()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");

        }

        [TestMethod]
        public void Index_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        // --------------------------- AdminUser ---------------------

        // Tester for å sjekke AdminAdminUsers(int? id):

        [TestMethod]
        public void AdminAdminUsers_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.AdminAdminUsers(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void AdminAdminUsers_Found_User()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (ViewResult)controller.AdminAdminUsers(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void AdminAdminUsers_NOT_Found_User()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (ViewResult)controller.AdminAdminUsers(null);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        // Tester for å sjekke GetAdminUser(int id):
        [TestMethod]
        public void GetAdminUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (JsonResult)controller.GetAdminUser(1);

            // Assert
            Assert.IsNull(actionResult); //Får jo ikke sjekket om denne går til login-sia!!!!!!!!!! <-<

        }

        [TestMethod]
        public void GetAdminUser_Found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetAdminUser(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));

        }

        [TestMethod]
        public void GetAdminUser_NOT_found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetAdminUser(0);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));

        }

        // Tester for å sjekke CreateAdminUser():

        [TestMethod]
        public void createAdminUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateAdminUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void CreateAdminUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateAdminUser();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CreateAdminUser(AdminUser adminUser):

        [TestMethod]
        public void createAdminUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateAdminUser(ExpectedAdminUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void CreateAdminUser_post_error_model()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            controller.ViewData.ModelState.AddModelError("Firstname", "First name missing");
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateAdminUser(ExpectedAdminUser);
            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateAdminUser_insertOK_True()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "Test";
            ExpectedAdminUser.password = "Testing1";
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateAdminUser(ExpectedAdminUser);
            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminAdminUsers");
        }

        [TestMethod]
        public void CreateAdminUser_insertOK_False()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            controller.Session["LoggedInAdmin"] = true;
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act
            var actionResult = (ViewResult)controller.CreateAdminUser(ExpectedAdminUser);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke EditAminUser(int id):

        [TestMethod]
        public void editAdminUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditAdminUser(1, ExpectedAdminUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void EditAdminUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.EditAdminUser(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditAdminUser_Update_Failed()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act
            var actionResult = (ViewResult)controller.EditAdminUser(1, ExpectedAdminUser);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditAdminUser_Update_Worked()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "Test";
            ExpectedAdminUser.password = "Testing1";
            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAdminUser(1, ExpectedAdminUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminAdminUsers");
        }

        // Tester for å sjekke EditAdminUser(int id, AdminUser adminUser):


        // Tester for å sjekke DeleteAdminUser(int id):
        [TestMethod]
        public void deleteAdminUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.DeleteAdminUser(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void DeleteAdminUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteAdminUser(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminAdminUsers");

        }

        // Tester for å sjekke CancelAdminUser():
        [TestMethod]
        public void CancelAdminUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.CancelAdminUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CancelAdminUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CancelAdminUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminAdminUsers");
        }

        // -------------------------- User ---------------------------

        // Tester for å sjekke AdminCustomers(int? id):

        [TestMethod]
        public void AdminCustomers_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.AdminCustomers(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void AdminCustomers_Null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.AdminCustomers(null);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void AdminCustomers_NOT_Null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.AdminCustomers(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke GetUser(int id):

        [TestMethod]
        public void GetUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (JsonResult)controller.GetUser(1);

            // Assert
            Assert.IsNull(actionResult);
        }

        [TestMethod]
        public void GetUser_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            // Act
            var actionResult = (JsonResult)controller.GetUser(-1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult)); // Aner ikke om denne funker heller.


        }

        // Tester for å sjekke CreateUser():

        [TestMethod]
        public void createUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CreateUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateUser();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }


        [TestMethod]
        public void CreateUser_LogIn()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
        }

        // Tester for å sjekke CreateUser(User user):

        [TestMethod]
        public void CreateUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            var expectedUser = new User()
            {
                id = 1,
                firstName = "Test",
                lastName = "Testen",
                address = "Testveien 1",
                postalCode = "1234",
                city = "Test",
                email = "test@test.test",
                phoneNumber = "12345678",
                userName = "Test",
                password = "Testing1"
            };

            // Act
            var result = (RedirectToRouteResult)controller.CreateUser(expectedUser);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AdminCustomers");

        }



        // Tester for å sjekke EditUser(int id):

        [TestMethod]
        public void EditUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.EditUser(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke EditUser(int id, User user):

        [TestMethod]
        public void EditUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            var user = new User()
            {
                id = 1,
                firstName = "Test",
                lastName = "Testen",
                address = "Testveien 1",
                postalCode = "1234",
                city = "Test",
                email = "test@test.test",
                phoneNumber = "12345678",
                userName = "Test",
                password = "Testing1"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditUser(1, user);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        // Tester for å sjekke DeleteUser(int id):

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.DeleteUser(1);
            var result = (User)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CancelUser():

        [TestMethod]
        public void CancelUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // ----------------------------- Product -----------------------

        // Tester for å sjekke AdminProducts(int? id):

        [TestMethod]
        public void AdminProducts()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke GetProduct(int id):

        [TestMethod]
        public void GetProduct()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateProduct():

        [TestMethod]
        public void CreateProduct()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.CreateProduct();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CreateProduct(Product product):

        [TestMethod]
        public void CreateProduct_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditProduct(int id):

        [TestMethod]
        public void EditProduct()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.EditProduct(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        // Tester for å sjekke EditProduct(int id, Product product):

        [TestMethod]
        public void EditProduct_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke DeleteProduct(int id):

        [TestMethod]
        public void DeleteProduct()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.DeleteProduct(1);
            var result = (Product)actionResult.Model; 

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CancelProduct():

        [TestMethod]
        public void CancelProduct()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // ------------------------------ Order and Orderline --------------------

        // Tester for å sjekke AdminOrders(int id):

        [TestMethod]
        public void AdminOrders()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke GetOrder(int id):

        [TestMethod]
        public void GetOrder()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateOrder():

        [TestMethod]
        public void CreateOrder()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.CreateOrder();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CreateOrder(Order order):

        [TestMethod]
        public void CreateOrder_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditOrder(int id):

        [TestMethod]
        public void EditOrder()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.EditOrder(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke EditOrder(int id, Order order):

        [TestMethod]
        public void EditOrder_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke DeleteOrder(int id):

        [TestMethod]
        public void DeleteOrder()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.DeleteOrder(1);
            var result = (Order)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        // Tester for å sjekke CancelOrder():

        [TestMethod]
        public void CancelOrder()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }
    }
}