﻿using NUnit.Framework;
using Labinator2016.Lib.Models;
using Labinator2016.Lib.Headers;
using System.Web;

namespace Labinator2016.Tests.TestData
{
    [TestFixture]
    public class LoggingSystem
    {
        [Test]
        public void LogWriteTest()
        {
            // Arrange
            FakeDatabase db = new FakeDatabase();
            HttpContextBase cx = new FakeHttpContext();

            // Act
            Log.Write(db, cx, new Log() {Detail="TestMessage", Message=LogMessages.connected });

            // Assert
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(LogMessages.connected, ((Log)db.LogAdded[0]).Message);
            Assert.AreEqual("TestMessage", ((Log)db.LogAdded[0]).Detail);

        }
    }
}
