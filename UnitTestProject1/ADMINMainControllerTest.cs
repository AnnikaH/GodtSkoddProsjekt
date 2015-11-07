using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using GodtSkoddProsjekt.Controllers;
using Model;
using BLL;
using DAL;

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
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke LoggedIn():

        [TestMethod]
        public void LoggedIn()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CheckLogIn(String username, String password):

        [TestMethod]
        public void CheckLogIn()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke LogOut():

        [TestMethod]
        public void LogOut()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // --------------------------- Index --------------------------

        // Tester for å sjekke Index():

        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // --------------------------- AdminUser ---------------------

        // Tester for å sjekke AdminAdminUsers(int? id):

        [TestMethod]
        public void AdminAdminUsers()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke GetAdminUser(int id):

        [TestMethod]
        public void GetAdminUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateAdminUser():

        [TestMethod]
        public void CreateAdminUser_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateAdminUser(AdminUser adminUser):

        [TestMethod]
        public void CreateAdminUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditAminUser(int id):

        [TestMethod]
        public void EditAdminUser_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditAdminUser(int id, AdminUser adminUser):

        [TestMethod]
        public void EditAdminUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke DeleteAdminUser(int id):

        [TestMethod]
        public void DeleteAdminUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CancelAdminUser():

        [TestMethod]
        public void CancelAdminUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // -------------------------- User ---------------------------

        // Tester for å sjekke AdminCustomers(int? id):

        [TestMethod]
        public void AdminCustomers()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke GetUser(int id):

        [TestMethod]
        public void GetUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateUser():

        [TestMethod]
        public void CreateUser_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke CreateUser(User user):

        [TestMethod]
        public void CreateUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditUser(int id):

        [TestMethod]
        public void EditUser_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke EditUser(int id, User user):

        [TestMethod]
        public void EditUser_ok_post()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        // Tester for å sjekke DeleteUser(int id):

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

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
        public void CreateProduct_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

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
        public void EditProduct_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

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

            // Assert

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
        public void CreateOrder_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

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
        public void EditOrder_show_view()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

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

            // Assert

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