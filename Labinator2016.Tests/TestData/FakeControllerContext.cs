namespace Labinator2016.Tests.TestData
{
    using System.Security.Principal;
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

        private FakeUser user = new FakeUser();
        public override IPrincipal User
        {
            get
            {
                return user;
            }

            set
            {
                base.User = value;
            }
        }
    }

    public class FakeUser : System.Security.Principal.IPrincipal
    {
        private FakeIdentity identity = new FakeIdentity();
        public bool IsInRole(string str)
        {
            return false;
        }
        public System.Security.Principal.IIdentity Identity
        {
            get
            {
            return identity;
            }
}
        
    }

    public class FakeIdentity : System.Security.Principal.IIdentity
    {
        public string Name
        {
            get
            {
                return "Test User";
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Test";
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return true;
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