using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labinator2016.Lib.Headers;

namespace Labinator2016.Tests.TestData
{
    class FakeFormAuthenticator : IAuth
    {
        public void DoAuth(string userName, bool remember)
        {
        }
        public void DoDeAuth() { }
    }
}
