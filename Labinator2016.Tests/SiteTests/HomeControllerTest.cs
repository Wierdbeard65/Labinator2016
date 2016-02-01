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
    using NUnit.Framework;

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
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, ((IndexViewModel)result.Model).ShowAll);
        }

        /// <summary>
        /// Home the controller post back own classes only index test.
        /// </summary>
        [Test]
        public void HomeControllerPostbackOwnClassesOnlyIndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(new IndexViewModel() { ShowAll = false }) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, ((IndexViewModel)result.Model).ShowAll);
        }

        /// <summary>
        /// Home the controller post back all classes index test.
        /// </summary>
        [Test]
        public void HomeControllerPostbackAllClassesIndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(new IndexViewModel() { ShowAll = true }) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, ((IndexViewModel)result.Model).ShowAll);
        }
    }
}