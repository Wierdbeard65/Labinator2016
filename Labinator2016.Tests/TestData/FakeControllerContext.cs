namespace Labinator2016.Tests.TestData
{
    using System.Web;
    using System.Web.Mvc;

    class FakeControllerContext : ControllerContext
    {
        HttpContextBase _context = new FakeHttpContext();
        public override HttpContextBase HttpContext
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }
    }
    class FakeHttpContext : HttpContextBase
    {
        HttpRequestBase _request = new FakeHttpRequest();
        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }
    }

    class FakeHttpRequest : HttpRequestBase
    {
        public override string this[string key]
        {
            get
            {
                return null;
            }
        }

        public override System.Collections.Specialized.NameValueCollection Headers
        {
            get
            {
                return new System.Collections.Specialized.NameValueCollection();
            }
        }
    }
}