using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeCon_webapp.Controllers;
using System.Web.Mvc;
using HeCon_webapp.Models;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Data.Entity;

namespace Hecon_webapp.Tests.Controllers
{
    /// <summary>
    /// Summary description for DoctorProfileTest
    /// </summary>
    [TestClass]
    public class DoctorProfileTest
    {
        public DoctorProfileTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        public void NewTestWhenReturnView()
        {
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);
            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);
            DoctorProfileController controller = new DoctorProfileController();
            controller.ControllerContext = controllerContext.Object;
            var result =  controller.New() as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(DoctorProfile));
        }
        [TestMethod]
        public void NewTestWhenNewProfileIsCreated()
        {
            DoctorProfile dr = new DoctorProfile();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<DoctorProfile>>();
            dbSetMock.Setup(m => m.Add(dr));
            dbContextMock.Setup(m => m.DoctorsProfiles).Returns(dbSetMock.Object);
            dbContextMock.Setup(m => m.DoctorsProfiles.Add(dr));
            DoctorProfileController controller = new DoctorProfileController(dbContextMock.Object);
            controller.New(dr);
            dbContextMock.Verify(m => m.DoctorsProfiles.Add(dr), Times.Once());
        }
    }
}
