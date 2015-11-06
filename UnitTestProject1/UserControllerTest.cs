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
        public void LogIn_show()
        {
            // Arrange
            //var controller = new ADMINMainController(new BusinessLogic(new RepositoryStub));

            
        }

        [TestMethod]
        public void GetUser()
        {
            // var controller = new DAL.DBGodtSkodd(new BLL.BusinessLogic(new DAL.RepositoryStub()));
        }

        [TestMethod]
        public void GetUsers() //alle kunder
        {
            // var controller = new DAL.DBGodtSkodd(new BLL.BusinessLogic(new DAL.RepositoryStub()));
            //var expecedResult = new List<User>();
            //var user = new User()
            /*
            {
                 id = 1,
                 firstName = "Test",
                 lastName = "Testen",
                 address = "Testveien 1",
                 postalCode= "1234",
                 city = "Test",
                 userName = "Test",
                 password = "Testing"
          
            }
          expectedResult.Add(user);
          expectedResult.Add(user);
          expectedResult.Add(user);  
            
            */

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
