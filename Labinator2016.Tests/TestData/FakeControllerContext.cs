namespace Labinator2016.Tests.TestData
{
    using System.Web;
    using System.Web.Mvc;

    public class FakeControllerContext : ControllerContext
    {
        private HttpContextBase context = new FakeHttpContext();

        public override HttpContextBase HttpContext
        {
            get
            {
                return this.context;
            }

            set
            {
                this.context = value;
            }
        }
    }

    public class FakeHttpContext : HttpContextBase
    {
        private HttpRequestBase request = new FakeHttpRequest();

        public override HttpRequestBase Request
        {
            get
            {
                return this.request;
            }
        }
    }

    public class FakeHttpRequest : HttpRequestBase
    {
        public override System.Collections.Specialized.NameValueCollection Headers
        {
            get
            {
                return new System.Collections.Specialized.NameValueCollection();
            }
        }

        public override string this[string key]
        {
            get
            {
                return null;
            }
        }
    }
}