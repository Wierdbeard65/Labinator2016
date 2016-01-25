using System;
using System.Collections.Generic;
using Labinator.Lib.REST;
using Labinator2016.Tests.TestData;
using NUnit.Framework;
using RestSharp;

namespace Labinator2016.Tests.TestHarness
{
    [TestFixture]
    public class FakeVmTest
    {
        [Test]
        public void FakeVmExecuteTest()
        {
            var st = new FakeSkyTap();
            st.AddSet(TestVmRESTData.vms);
            var request = new RestRequest();
            var reply = st.Execute<Vm>(request);
            Assert.IsNotNull(reply);
            Assert.AreSame(typeof(Vm), reply.GetType());
        }
        [Test]
        public void FakeVmExecuteListTest()
        {
            var st = new FakeSkyTap();
            st.AddSet(TestVmRESTData.vms);
            var request = new RestRequest();
            var reply = st.ExecuteList<Vm>(request);
            Assert.IsNotNull(reply);
            Assert.AreEqual(typeof(List<Vm>), reply.GetType());
            Assert.AreEqual(10, reply.Count);
            Assert.AreEqual(typeof(Vm), (reply[0]).GetType());
        }
    }
}
