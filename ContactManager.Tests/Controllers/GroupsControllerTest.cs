using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactManager.Controllers;
using System.Web.Mvc;

namespace ContactManager.Tests.Controllers
{
    [TestClass]
    public class GroupsControllerTest
    {
        [TestMethod]
        public void Create()
        {
            GroupsController test = new GroupsController();
            ViewResult result = test.Create() as ViewResult;
            //result.ExecuteResult();
        }
    }
}
