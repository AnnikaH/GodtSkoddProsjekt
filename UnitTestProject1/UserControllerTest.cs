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
            //var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub));

            
        }

        [TestMethod]
        public void GetUsers()
        {

        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            //var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub));


        }

        [TestMethod]
        public void CreateUser()
        {
            // var controller = new DAL.DBGodtSkodd(new BLL.BusinessLogic(new DAL.RepositoryStub()));
        }

        [TestMethod]
        public void EditUser()
        {
            // var controller = new DAL.DBGodtSkodd(new BLL.BusinessLogic(new DAL.RepositoryStub()));
        }
    }
}
