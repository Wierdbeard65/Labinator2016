//-----------------------------------------------------------------------
// <copyright file="HomeControllerTest.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Tests.Controllers
{
    using System.Web.Mvc;
    using Labinator2016.Controllers;
    using Labinator2016.ViewModels;
    using Lib.Headers;
    using NUnit.Framework;
    using TestData;
    /// <summary>
    /// Unit tests for the Home Controller
    /// </summary>
    [TestFixture]
    public class HomeControllerTest
    {
        /// <summary>
        /// Home the controller initial index test.
        /// </summary>
        [Test]
        public void HomeControllerInitialIndexTest()
        {
            // Arrange
            var db = new FakeDatabase();
            db.AddSet(TestUserData.Users);
            HomeController controller = new HomeController(db);
            controller.ControllerContext = new FakeControllerContext();
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}