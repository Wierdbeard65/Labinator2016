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

    /// <summary>
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
    }
}