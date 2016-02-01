
namespace Labinator2016.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Labinator2016.Tests.TestData;
    using Labinator2016.Lib.Models;
    using Labinator2016.Controllers;
    using NUnit.Framework;
    using ViewModels.DatatablesViewModel;
    using Lib.REST;
    [TestFixture]
    public class CourseControllerTest
    {
        [Test]
        public void CourseAjaxList()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() { Start = 10, Length = 5, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } } };
            JsonResult result = controller.Ajax(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, ((List<Course>)((DTResult<Course>)result.Data).data).Count);
            Assert.AreEqual("Test11", ((List<Course>)((DTResult<Course>)result.Data).data)[0].Name);
        }
        [Test]
        public void MachineAjaxList()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseMachineTempData.CourseMachineTemps);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() { Start = 2, Length = 5, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } }, Course = 1, Session = "12345" };
            JsonResult result = controller.MachineAjax(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, ((List<CourseMachineTemp>)((DTResult<CourseMachineTemp>)result.Data).data).Count);
            Assert.AreEqual("Test3", ((List<CourseMachineTemp>)((DTResult<CourseMachineTemp>)result.Data).data)[0].VMName);
        }
        [Test]
        public void CourseControllerIndexTest()
        {
            // Arrange
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            CoursesController controller = new CoursesController(db, st);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void CourseEditNewStartTest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            db.AddSet(TestCourseMachineTempData.CourseMachineTemps);
            st.AddSet(TestTemplateRESTData.templates);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(11, ((List<Template>)((SelectList)result.ViewBag.Template).Items).Count);
            Assert.AreEqual(typeof(Course), result.Model.GetType());
            Assert.AreEqual("New", ((Course)result.Model).Name);
        }
        [Test]
        public void CourseEditExistingStartTest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            db.AddSet(TestCourseMachineTempData.CourseMachineTemps);
            db.AddSet(TestCourseMachineData.CourseMachines);
            st.AddSet(TestTemplateRESTData.templates);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(11, ((List<Template>)((SelectList)result.ViewBag.Template).Items).Count);
            Assert.AreEqual(typeof(Course), result.Model.GetType());
            Assert.AreEqual("Test1", ((Course)result.Model).Name);
        }
        [Test]
        public void CourseEditNewWriteTest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            db.AddSet(TestCourseMachineData.CourseMachines);
            db.AddSet(TestCourseMachineTempData.CourseMachineTemps);
            st.AddSet(TestTemplateRESTData.templates);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            Course testCourse = new Course() { CourseId = 0, Name = "TestNew", Days = 5, Hours = 8, Template = "11111111" };
            var result = controller.Edit(testCourse, Guid.NewGuid().ToString());
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(1, db.Added.Count);
            Assert.AreEqual("TestNew", ((Course)db.Added[0]).Name);
            Assert.AreEqual(2, db.saved);
        }
        [Test]
        public void CourseEditExistingWriteTest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            db.AddSet(TestCourseMachineData.CourseMachines);
            db.AddSet(TestCourseMachineTempData.CourseMachineTemps);
            st.AddSet(TestTemplateRESTData.templates);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            Course testCourse = new Course() { CourseId = 1, Name = "TestUpdate", Days = 5, Hours = 8, Template = "11111111" };
            var result = controller.Edit(testCourse, Guid.NewGuid().ToString());
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(0, db.Added.Count);
            Assert.AreEqual(1, db.Updated.Count);
            Assert.AreEqual("TestUpdate", ((Course)db.Updated[0]).Name);
            Assert.AreEqual(2, db.saved);
        }
        [Test]
        public void CourseDeleteStartTest()
        {
            var db = new FakeDatabase();
            var st = new FakeSkyTap();
            db.AddSet(TestCourseData.Courses);
            db.AddSet(TestCourseMachineData.CourseMachines);
            var controller = new CoursesController(db, st);
            controller.ControllerContext = new FakeControllerContext();
            var result = controller.Delete(1);
            Assert.IsNotNull(result);
        }
    }
}