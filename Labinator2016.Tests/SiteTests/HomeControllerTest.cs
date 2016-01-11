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
    //using Labinator2016.ViewModels;
    using NUnit.Framework;
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void HomeConttollerIndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(false, ((IndexViewModel)result.Model).ShowAll);
        }
    }
}