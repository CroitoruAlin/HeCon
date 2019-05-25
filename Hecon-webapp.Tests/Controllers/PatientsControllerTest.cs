using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeCon_webapp.Controllers;
using System.Web.Mvc;
using HeCon_webapp.Models;
using System.Security.Principal;
using System.Web;
using Moq;
using System.Data.Entity;

namespace Hecon_webapp.Tests.Controllers
{
    /// <summary>
    /// Summary description for PatientsControllerTest
    /// </summary>
    [TestClass]
    public class PatientsControllerTest
    {
        public PatientsControllerTest()
        {
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
        public void NewTest()
        {
            PatientProfileController controller = new PatientProfileController();
            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);
            controller.ControllerContext = controllerContext.Object;
            ViewResult result = controller.New() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model,typeof(PatientProfile));
        }
        [TestMethod]
        public void NewTestWhenSavesData()
        {
            var patientProfile = new Mock< PatientProfile>();
            var mockSet = new Mock<DbSet<PatientProfile>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.PatientsProfiles).Returns(mockSet.Object);
            mockContext.Setup(m => m.PatientsProfiles.Add(patientProfile.Object));   
            PatientProfileController profileController = new PatientProfileController(mockContext.Object);
            profileController.New(patientProfile.Object);
            mockContext.Verify(m => m.PatientsProfiles.Add(patientProfile.Object), Times.Once());

        }
        [TestMethod]
        public void ShowTest()
        {
            var mockSet = new Mock<DbSet<PatientProfile>>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);
            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.PatientsProfiles).Returns(mockSet.Object);
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);
            PatientProfileController homeController = new PatientProfileController(mockContext.Object);
            homeController.ControllerContext = controllerContext.Object;
            var result = homeController.Show() as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "New");
        }
    }
}
