﻿using System;
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
        
        // ----------------------------- Log in/out --------------------------

        // Tests to check LogIn():

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

        // Tests to check LoggedIn():

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

        // Tests to check CheckLogIn(String username, String password):

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

        // Tests to check LogOut():

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

        // Tests to check Index():

        [TestMethod]
        public void Index()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

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

        // Tests to check AdminAdminUsers(int? id):

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

        // Tests to check GetAdminUser(int id):
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
            Assert.IsNull(actionResult);
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
            Assert.IsInstanceOfType(actionResult.Data, typeof(AdminUser));

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
            Assert.AreEqual(actionResult.Data, "");

        }

        // Tests to check CreateAdminUser(): and CreateAdminUser(AdminUser adminUser):

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

        // Tests to check EditAminUser(int id): and EditAdminUser(int id, AdminUser adminuser)

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
        public void editAdminUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedAdminUser = new AdminUser();
            ExpectedAdminUser.userName = "";
            ExpectedAdminUser.password = null;
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditAdminUser(1);

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


        // Tests to check DeleteAdminUser(int id):
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

        // Tests to check CancelAdminUser():
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

        // Tests to check AdminCustomers(int? id):

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

        // Tests to check GetUser(int id):

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
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.AreEqual(actionResult.Data, "");

        }
        [TestMethod]
        public void GetUser_NOT_null()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            // Act
            var actionResult = (JsonResult)controller.GetUser(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.IsInstanceOfType(actionResult.Data, typeof(User));


        }

        // Tests to check CreateUser(): and CreateUser(User user)

        [TestMethod]
        public void CreateUser_LoggedIn_Fail()
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
        public void CreateUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateUser(ExpectedUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CreateUser_Model_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            controller.ViewData.ModelState.AddModelError("Firstname", "First name missing");
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateUser(ExpectedUser);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateUser_Model_Valid()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            ExpectedUser.firstName = "Test";
            ExpectedUser.lastName = "Testen";
            ExpectedUser.address = "Testveien 1";
            ExpectedUser.email = "test@test.test";
            ExpectedUser.city = "Test";
            ExpectedUser.phoneNumber = "12345678";
            ExpectedUser.postalCode = "1234";
            ExpectedUser.userName = "Test";
            ExpectedUser.password = "Testing1";

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateUser(ExpectedUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminCustomers");
        }


        // Tests to check EditUser(int id): and EditUser(int id, User user):

        [TestMethod]
        public void EditUser_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditUser(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void EditUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.EditUser(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditUser(1, ExpectedUser);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void EditUser_model_valid()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller); 
            controller.Session["LoggedInAdmin"] = true;
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
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminCustomers");

        }


        [TestMethod]
        public void EditUser_model_NOT_valid()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedUser = new User();
            controller.ViewData.ModelState.AddModelError("Firstname", "First name missing");
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.EditUser(1, ExpectedUser);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }


        // Tests to check DeleteUser(int id):

        [TestMethod]
        
        public void DeleteUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteUser(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteUser(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminCustomers");
        }

        // Tests to check CancelUser():

        [TestMethod]
        public void CencelUser_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
        
            // Act
            var actionResult = (RedirectToRouteResult)controller.CancelUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void CancelUser()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CancelUser();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminCustomers");

        }

        // ----------------------------- Product -----------------------


        // Tests to check AdminProducts(int? id):

        [TestMethod]
        public void AdminProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.AdminProducts(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }
        [TestMethod]
        public void AdminProduct_Found_User()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (ViewResult)controller.AdminProducts(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void AdminProduct_NOT_Found_User()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (ViewResult)controller.AdminProducts(null);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        // Tests to check GetProduct(int id):
        [TestMethod]
        public void GetProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (JsonResult)controller.GetProduct(1);

            // Assert
            Assert.IsNull(actionResult); 
        }

        [TestMethod]
        public void GetProduct_Found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.IsInstanceOfType(actionResult.Data, typeof(Product));


        }

        [TestMethod]
        public void GetProduct_Not_Found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetProduct(0);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.AreEqual(actionResult.Data, "");

        }



        // Tests to check CreateProduct(): and CreateProduct(Product product):
        [TestMethod]
        public void CreateProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateProduct();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }

    

        [TestMethod]
        public void CreateProduct()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateProduct();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }



        [TestMethod]
        public void CreateProduct_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedProduct = new Product();
            ExpectedProduct.name = "";
            var actionResult = (RedirectToRouteResult)controller.CreateProduct(ExpectedProduct);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }

        [TestMethod]
        public void CreateProduct_post_error_model()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedProduct = new Product();
            controller.ViewData.ModelState.AddModelError("Name", "Name missing");
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateProduct(ExpectedProduct);
            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateProduct_insertOK_True()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedProduct = new Product();
            ExpectedProduct.name = "Shoe";
            ExpectedProduct.price = 499;
            ExpectedProduct.size = 45;
            ExpectedProduct.color = "Black";
            ExpectedProduct.material = "Mesh";
            ExpectedProduct.brand = "Brand";
            ExpectedProduct.url = "/Here";
            ExpectedProduct.gender = "Men";
            ExpectedProduct.type = "Sneakers";

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateProduct(ExpectedProduct);
            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminProducts");
        }


        [TestMethod]
        public void CreateProduct_insertOK_False()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedProduct = new Product();
            controller.Session["LoggedInAdmin"] = true;
            ExpectedProduct.name = "";
            var actionResult = (ViewResult)controller.CreateProduct(ExpectedProduct);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditProduct(0);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }


        // Tests to check EditProduct(int id): and EditProduct(int id, Product product):
        [TestMethod]
        public void EditProduct_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            Product Expectedproduct = new Product();
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditProduct(0, Expectedproduct);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }

        [TestMethod]
        public void EditProduct()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.EditProduct(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void EditProduct_Update_Failed()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedProduct = new Product();
            //ExpectedProduct.name = "";

            // Act
            var actionResult = (ViewResult)controller.EditProduct(0, ExpectedProduct);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditProduct_Update_Worked()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedProduct = new Product();
            ExpectedProduct.name = "Shoe";
            ExpectedProduct.price = 499;
            ExpectedProduct.size = 45;
            ExpectedProduct.color = "Black";
            ExpectedProduct.material = "Mesh";
            ExpectedProduct.brand = "Brand";
            ExpectedProduct.url = "/Here";
            ExpectedProduct.gender = "Men";
            ExpectedProduct.type = "Sneakers";

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditProduct(1, ExpectedProduct);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminProducts");
        }

        // Tests to check DeleteProduct(int id):

        [TestMethod]
        public void deleteProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.DeleteProduct(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void DeleteProduct()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteProduct(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminProducts");

        }
        // Tests to check CancelProduct():


        [TestMethod]
        public void cancelProduct_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CancelProduct();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CancelProduct()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CancelProduct();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminProducts");

        }


        // ------------------------------ Order and Orderline --------------------

        //Tests to check AdminOrders(int id):

        [TestMethod]
        public void AdminOrders_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.AdminOrders(0);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void AdminOrders_found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrder = new Order();
            ExpectedOrder.id = 1;
            controller.Session["Order"] = ExpectedOrder;
            controller.Session["UserIdForOrders"] = 1;

            // Act
            var actionResult = (ViewResult)controller.AdminOrders(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }
        [TestMethod]
        public void AdminOrders_not_found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            // Act
            var actionResult = (ViewResult)controller.AdminOrders(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        // Tests to check GetOrder(int id):

        [TestMethod]
        public void GetOrders_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (JsonResult)controller.GetAdminUser(1);
            // Assert
            Assert.IsNull(actionResult);
        }

            [TestMethod]
        public void GetOrder_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (JsonResult)controller.GetOrder(1);
            // Assert
            Assert.IsNull(actionResult);
        }

        [TestMethod]
        public void GetOrder_found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["UserIdForOrders"] = 1;
            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetOrder(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.IsInstanceOfType(actionResult.Data, typeof(Order));

        }

        [TestMethod]
        public void GetOrder_id_found()
        {
            
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["UserIdForOrders"] = 1;
            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetOrder(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.IsInstanceOfType(actionResult.Data, typeof(Order));
        }


        [TestMethod]
        public void GetOrder_not_found()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["UserIdForOrders"] = 1;
            controller.Session["LoggedInAdmin"] = true;
            // Act

            var actionResult = (JsonResult)controller.GetOrder(0);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            Assert.AreEqual(actionResult.Data, "");

        }


        // Tests to check CreateOrder():


        [TestMethod]
        public void CreateOrder_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateOrder();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CreateOrder()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            controller.Session["UserIdForOrders"] = 1;

            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateOrder();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");
        }

        //Tests to check EditOrder(int id) and EditOrder(int id, Order order)
        [TestMethod]
        public void EditOrder_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditOrder(0);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void EditOrder_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            Order ExpectedOrder = new Order();
            // Act

            var actionResult = (RedirectToRouteResult)controller.EditOrder(0, ExpectedOrder);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }


        [TestMethod]
        public void EditOrder()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.EditOrder(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditOrder_Update_Worked()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;

            var orderLines = new List<Orderline>();
            var ExpectedOrder = new Order();
            ExpectedOrder.id = 1;
            ExpectedOrder.userID = 1;
            ExpectedOrder.date = new DateTime();
            ExpectedOrder.orderlines = orderLines;

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditOrder(1, ExpectedOrder);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");
        }


        [TestMethod]
        public void EditOrder_Update_Failed()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrder = new Order();
            //ExpectedProduct.name = "";

            // Act
            var actionResult = (ViewResult)controller.EditOrder(0, ExpectedOrder);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }




        // Tests to check DeleteOrder(int id):


        [TestMethod]
        public void deleteOrder_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.DeleteOrder(0);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }


        [TestMethod]
        public void DeleteOrder()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            controller.Session["UserIdForOrders"] = 1;

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteOrder(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");
        }

        // Tests to check CancelOrder():

        [TestMethod]
        public void cancelOrder_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.CancelOrder();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }


        [TestMethod]
        public void CancelOrder()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            controller.Session["UserIdForOrders"] = 1;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CancelOrder();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");

        }

        //---- Orderlines

        //Tests to check CreateOrderLine
        [TestMethod]
        public void CreateOrderline()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            // Act
            var actionResult = (ViewResult)controller.CreateOrderline(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateOrderline_with_errormessage()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            controller.Session["ErrorMessageOrderline"] = "Test";
            // Act
            var actionResult = (ViewResult)controller.CreateOrderline(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateOrderline_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedOrderline = new Orderline();
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CreateOrderline_LoggedIn_Fail_too()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            var ExpectedOrderline = new Orderline();
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session_with_order_with_null_product()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            ExpectedOrderline.productId = 0;
            var ExpectedOrder = new Order();
            ExpectedOrder.id = 1;
            controller.Session["Order"] = ExpectedOrder;
            controller.Session["UserIdForOrders"] = 1;
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "CreateOrderline");
        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session_with_order()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            var ExpectedOrder = new Order();
            ExpectedOrder.id = 1;
            controller.Session["Order"] = ExpectedOrder;
            controller.Session["UserIdForOrders"] = 1;
            // Act

            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "CreateOrderline");
        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session_NOT_with_order()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            ExpectedOrderline.productId = 0;
            controller.Session["UserIdForOrders"] = 1;
            // Act

            var actionResult = (ViewResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session_with_order_with_product()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            ExpectedOrderline.productId = 1;
            var ExpectedOrder = new Order();
            controller.Session["Order"] = ExpectedOrder;
            controller.Session["UserIdForOrders"] = 1;
            // Act

            var actionResult = (ViewResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session_with_order_with_product_insertOK()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            ExpectedOrderline.productId = 1;
            ExpectedOrderline.id = 1;
            var ExpectedOrder = new Order();
            controller.Session["Order"] = ExpectedOrder;
            controller.Session["UserIdForOrders"] = 1;
            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");
        }

        [TestMethod]
        public void CreateOrderline_model_valid()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            // Act

            var actionResult = (ViewResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateOrderline_model_valid_with_session()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrder = new Order();
            ExpectedOrder.id = 1;
            controller.Session["Order"] = ExpectedOrder;
            var ExpectedOrderline = new Orderline();
            //ExpectedOrderline.productId = 0;

            var actionResult = (RedirectToRouteResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "CreateOrderline");
        }

        [TestMethod]
        public void CreateOrderline_model_valid_no_session()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            // Act

            var actionResult = (ViewResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void CreateOrderline_model_NOT_valid()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            var ExpectedOrderline = new Orderline();
            // Act

            var actionResult = (ViewResult)controller.CreateOrderline(ExpectedOrderline);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        //Tests to check DeleteOrderLine(int id)
        [TestMethod]
        public void DeleteOrderLine_LoggedIn_Fail()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            // Act

            var actionResult = (RedirectToRouteResult)controller.DeleteOrderline(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void DeleteOrderline()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedInAdmin"] = true;
            controller.Session["UserIdForOrders"] = 1;
            var actionResult = (RedirectToRouteResult)controller.DeleteOrderline(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(actionResult.RouteValues.Values.ElementAt(1), "AdminOrders");
        }
    }
}