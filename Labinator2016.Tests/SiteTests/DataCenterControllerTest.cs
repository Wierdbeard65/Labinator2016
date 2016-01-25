using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Labinator2016.Tests.TestData;
using Labinator2016;
using Labinator2016.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using Labinator2016.ViewModels;
using Labinator2016.Lib.Models;
using Labinator2016.ViewModels.DatatablesViewModel;

namespace Labinator2016.Tests.Controllers
{
    [TestClass]
    public class DataCenterControllerTest
    {
        [TestMethod]
        public void DataCenterAjaxList()
        {
            var db = new FakeDatabase();
            db.AddSet(TestDataCenterData.DataCenters);
            var controller = new DataCentersController(db);
            controller.ControllerContext = new FakeControllerContext();
            DTParameters param = new DTParameters() { Start = 2, Length = 3, Search = new DTSearch(), Order = new DTOrder[1] { new DTOrder() { Column = 1, Dir = DTOrderDir.ASC } } };
            JsonResult result = controller.Ajax(param) as JsonResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, ((List<DataCenter>)((DTResult<DataCenter>)result.Data).data).Count);
            Assert.AreEqual("Test2", ((List<DataCenter>)((DTResult<DataCenter>)result.Data).data)[0].Name);
        }
        [TestMethod]
        public void DataCentersControllerIndexTest()
        {
            // Arrange
            DataCentersController controller = new DataCentersController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DataCenterEditNewStartTest()
        {
            var db = new FakeDatabase();
            db.AddSet(TestDataCenterData.DataCenters);
            var controller = new DataCentersController(db);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(DataCenter), result.Model.GetType());
            Assert.AreEqual("New", ((DataCenter)result.Model).Name);
        }
        [TestMethod]
        public void DataCenterEditExistingStartTest()
        {
            var db = new FakeDatabase();
            db.AddSet(TestDataCenterData.DataCenters);
            var controller = new DataCentersController(db);
            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(DataCenter), result.Model.GetType());
            Assert.AreEqual("Test1", ((DataCenter)result.Model).Name);
        }
        [TestMethod]
        public void DataCenterEditNewWriteTest()
        {
            var db = new FakeDatabase();
            db.AddSet(TestDataCenterData.DataCenters);
            var controller = new DataCentersController(db);
            controller.ControllerContext = new FakeControllerContext();
            DataCenter testDataCenter = new DataCenter() { DataCenterId = 0, Name = "TestNew" };
            var result = controller.Edit(testDataCenter);
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(1, db.Added.Count);
            Assert.AreEqual("TestNew", ((DataCenter)db.Added[0]).Name);
            Assert.AreEqual(1, db.saved);
        }
        [TestMethod]
        public void DataCenterEditExistingWriteTest()
        {
            var db = new FakeDatabase();
            db.AddSet(TestDataCenterData.DataCenters);
            var controller = new DataCentersController(db);
            controller.ControllerContext = new FakeControllerContext();
            DataCenter testDataCenter = new DataCenter() { DataCenterId = 1, Name = "TestChange" };
            var result = controller.Edit(testDataCenter);
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(0, db.Added.Count);
            Assert.AreEqual(1, db.Updated.Count);
            Assert.AreEqual("TestChange", ((DataCenter)db.Updated[0]).Name);
            Assert.AreEqual(1, db.saved);
        }
    }
}
