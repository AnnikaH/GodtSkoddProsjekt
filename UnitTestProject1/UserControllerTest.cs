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
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        [TestMethod]
        public void GetUsers()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }

        [TestMethod]
        public void CreateUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert
        }

        [TestMethod]
        public void EditUser()
        {
            // Arrange
            var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub()));

            // Act

            // Assert

        }
    }
}