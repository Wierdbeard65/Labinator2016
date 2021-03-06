﻿//-----------------------------------------------------------------------
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
    using System.Web.Script.Serialization;
    /// <summary>
    /// Unit tests for the Classroom Controller
    /// </summary>
    [TestFixture]
    public class ClassroomsControllerTest
    {
        [Test]
        public void ClassroomAjaxList()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestClassroomData.Classrooms);
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() { Start = 2, Length = 3, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } } };
            JsonResult result = controller.Ajax(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, ((List<Classroom>)((DTResult<Classroom>)result.Data).data).Count);
        }

        [Test]
        public void AjaxSeatList()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() {SessionId= "12345", Start = 2, Length = 3, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } } };
            JsonResult result = controller.AjaxSeat(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, ((List<SeatTemp>)((DTResult<SeatTemp>)result.Data).data).Count);
        }

        [Test]
        public void EditStartNew()
        {
            var db = new FakeDatabase();
            db.AddSet(TestUserData.Users);
            db.AddSet(TestSeatData.Seats);
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestDataCenterData.DataCenters);
            db.AddSet(TestCourseData.Courses);
            var st = new FakeSkyTap();
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, ((Classroom)result.Model).ClassroomId);
        }

        [Test]
        public void EditStartExisting()
        {
            var db = new FakeDatabase();
            db.AddSet(TestClassroomData.Classrooms);
            db.AddSet(TestUserData.Users);
            db.AddSet(TestSeatData.Seats);
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestDataCenterData.DataCenters);
            db.AddSet(TestCourseData.Courses);
            var st = new FakeSkyTap();
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((Classroom)result.Model).ClassroomId);
            Assert.AreEqual("Test1", ((Classroom)result.Model).Project);
        }

        [Test]
        public void EditWriteBackNew()
        {
            var db = new FakeDatabase();
            db.AddSet(TestClassroomData.Classrooms);
            db.AddSet(TestUserData.Users);
            db.AddSet(TestSeatData.Seats);
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestDataCenterData.DataCenters);
            db.AddSet(TestCourseData.Courses);
            var st = new FakeSkyTap();
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            Classroom NewRoom = new Classroom() { ClassroomId = 0, CourseId = 1, DataCenterId = 1, Project = "ABCDE", Start = System.DateTime.Now, UserId = 1 };
            ActionResult result = controller.Edit(NewRoom, "12345");
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditWriteBackNewInvalidModel()
        {
            var db = new FakeDatabase();
            db.AddSet(TestClassroomData.Classrooms);
            db.AddSet(TestUserData.Users);
            db.AddSet(TestSeatData.Seats);
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestDataCenterData.DataCenters);
            db.AddSet(TestCourseData.Courses);
            var st = new FakeSkyTap();
            var controller = new ClassroomsController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            controller.ModelState.AddModelError("fakeError", "fakeError");
            Classroom NewRoom = new Classroom() { ClassroomId = 0, CourseId = 1, DataCenterId = 1, Project = "ABCDE", Start = System.DateTime.Now, UserId = 1 };
            ActionResult result = controller.Edit(NewRoom, "12345");
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddSeatTestValidSingleExistingUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser1@test.com','Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(1, db.Added.Count);
        }

        [Test]
        public void AddSeatTestValidMultipleExistingUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser1@test.com\nTestUser2@test.com','Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(2, db.Added.Count);
        }

        [Test]
        public void AddSeatTestValidSingleNewUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser999@test.com','Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(2, db.Added.Count);
        }

        [Test]
        public void AddSeatTestValidMultipleNewUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser999@test.com\nTestUser998@test.com','Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(4, db.Added.Count);
        }

        [Test]
        public void AddSeatTestValidEmptyUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'','Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Added.Count);
        }

        [Test]
        public void AddSeatTestInvalidNoUser()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'Session':'12345','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Added.Count);
        }

        [Test]
        public void AddSeatTestInvalidNoSession()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser999@test.com','Classroom':1}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Added.Count);
        }

        [Test]
        public void AddSeatTestInvalidNoClassroom()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser999@test.com','Session':'12345'}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Added.Count);
        }

        [Test]
        public void AddSeatTestInvalidClassroom()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestSeatTempData.SeatTemps);
            db.AddSet(TestUserData.Users);
            var controller = new ClassroomsController(db, st);
            var httpRequest = new Mock<HttpRequestBase>();
            var stream = new MemoryStream(Encoding.Default.GetBytes("{'NewSeats':'TestUser1@test.com\nTestUser2@test.com','Session':'12345','Classroom':'Invalid'}"));
            httpRequest.Setup(r => r.InputStream).Returns(stream);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request).Returns(httpRequest.Object);

            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(httpContext.Object, routeData, controller);
            var result = controller.AddSeats();
            dynamic jsonObject = result.Data;
            Assert.AreEqual("Done", jsonObject["Status"]);
            Assert.AreEqual(0, db.Added.Count);
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