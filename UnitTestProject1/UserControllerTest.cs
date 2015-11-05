using System;
using GodtSkoddProsjekt.Controllers;
using GodtSkoddProsjekt.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUser()
        {

        }

        [TestMethod]
        public void Delete()
        {
            var controller = new DAL.DBGodtSkodd(new BLL.BusinessLogic(new DAL.RepositoryStub()));
        }
    }
}
