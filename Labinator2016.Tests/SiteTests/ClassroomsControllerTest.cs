//-----------------------------------------------------------------------
// <copyright file="ClassroomsControllerTest.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Tests.SiteTests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Labinator2016.Controllers;
    using Labinator2016.Lib.Models;
    using Labinator2016.Tests.TestData;
    using Labinator2016.ViewModels.DatatablesViewModel;
    using NUnit.Framework;
    using System.Web;
    using System.IO;
    using System.Text;
    using System.Web.Routing;
    using Moq;
    using System.Web.Script.Serialization;/// <summary>
                                          /// Unit tests for the Classroom Controller
                                          /// </summary>
    [TestFixture]
    public class ClassroomsControllerTest
    {
        /// <summary>
        /// Classroom ajax list.
        /// </summary>
        [Test]
        public void ClassroomAjaxList()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestClassroomData.Classrooms);
            var controller = new ClassroomsController(db,st);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() { Start = 2, Length = 3, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } } };
            JsonResult result = controller.Ajax(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, ((List<Classroom>)((DTResult<Classroom>)result.Data).data).Count);
        }
        [Test]
        public void RemoveSeatTestExists()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'SeatTempId':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.RemoveSeat();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(1, db.Removed.Count);
        }
        [Test]
        public void RemoveSeatTestNotExists()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'SeatTempId':50}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.RemoveSeat();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Removed.Count);
        }
        [Test]
        public void RemoveSeatTestInvalidType()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'SeatTempId':'This One'}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.RemoveSeat();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Removed.Count);
        }
        [Test]
        public void RemoveSeatTestInvalidRequest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'WrongParameter':50}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.RemoveSeat();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Removed.Count);
        }
    }
}