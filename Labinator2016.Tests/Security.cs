using System;
using Labinator2016.Lib.Utilities;
using NUnit.Framework;

namespace Labinator2016.Tests
{
    [TestFixture]
    public class Security
    {
        [Test]
        public void PasswordHashTest()
        {
            string hashed = PasswordHash.CreateHash("Test");
            Assert.AreEqual(true, PasswordHash.ValidatePassword("Test",hashed));
            Assert.AreEqual(false, PasswordHash.ValidatePassword("NoTest", hashed));
        }
    }
}
