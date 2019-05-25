using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeCon_webapp.Models;
using System.Web;
using Moq;
using HeCon_webapp.Controllers;
using System.Data.Entity;
using System.Security.Principal;
using System.Web.Mvc;

namespace Hecon_webapp.Tests.Controllers
{
    public class TestPathProvider : IPathProvider
    {
        public string MapPath(string path)
        {
            return "~/UploadedImages/";
        }
    }
    [TestClass]
    public class XrayControllerTest
    {
        public XrayControllerTest()
        {
            
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NewTest()
        {
            XRay xray = new XRay();
            xray.ImageId = 1;
            xray.UserId = "";
            xray.Title = "title";
            var imageFile = new Mock<HttpPostedFileBase>();
            imageFile.Setup(m => m.FileName).Returns("filename");
            xray.ImageFile = imageFile.Object;
            var dbSetMock = new Mock<DbSet<XRay>>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);
            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);
            dbSetMock.Setup(m => m.Add(xray));
            dbContextMock.Setup(m => m.XRays).Returns(dbSetMock.Object);
            dbContextMock.Setup(m => m.XRays.Add(xray));
            XRayController controller = new XRayController(dbContextMock.Object,new TestPathProvider());
            controller.ControllerContext = controllerContext.Object;
            controller.New(xray);
            dbContextMock.Verify(m => m.XRays.Add(xray), Times.Once());

        }
    }
}
