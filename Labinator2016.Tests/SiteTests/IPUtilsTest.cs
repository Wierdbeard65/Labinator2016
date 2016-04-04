using System;
using Labinator2016.Lib.Utilities;
using NUnit.Framework;

namespace Labinator2016.Tests.SiteTests
{
    /// <summary>
    /// Summary description for IPUtilsTest
    /// </summary>
    [TestFixture]
    public class IPUtilsTest
    {
        [Test]
        public void NumericToStringIPTest()
        {
            string testAddress = IPUtils.NumericToStringIP(0xf00ff00f);
            Assert.AreEqual("240.15.240.15", testAddress);
        }
    }
}
