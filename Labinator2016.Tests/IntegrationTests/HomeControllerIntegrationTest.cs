//-----------------------------------------------------------------------
// <copyright file="HomeControllerIntegrationTest.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Web.Mvc;
    using MVCIntegrationTestFramework.Browsing;
    using NUnit.Framework;

    /// <summary>
    /// Integrations tests for the controllers.
    /// </summary>
    [TestFixture]
    public class HomeControllerIntegrationTest
    {
        /// <summary>
        /// Logins the redirect test.
        /// </summary>
        [Test]
        public void LoginRedirectTest()
        {
            string testURL;
            HttpWebRequest request;
            Assembly asm = Assembly.LoadFrom("D:\\Source\\Labinator2016\\Labinator2016\\bin\\Labinator2016.dll");
            IEnumerable<MethodInfo> conts = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)) // filter controllers
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && method.Module.ToString() == "Labinator2016.dll" && !method.IsDefined(typeof(NonActionAttribute)));
            foreach (MethodInfo testAction in conts)
            {
                testURL = testAction.DeclaringType.Name.Replace("Controller", string.Empty) + "/" + testAction.Name;
                switch (testURL)
                {
                    case "Users/Login":
                    case "Users/Ajax":
                    case "DataCenters/Ajax":
                    case "Courses/Ajax":
                    case "Courses/MachineAjax":
                    case "Courses/Refresh":
                    case "Courses/Active":
                    case "Classrooms/Ajax":
                    case "Classrooms/AjaxSeat":
                    case "Classrooms/SeatGrid":
                    case "Reports/Ajax":
                        break;
                    default:
                        request = (HttpWebRequest)HttpWebRequest.Create("http://localhost/Labinator2016/" + testURL);
                        request.AllowAutoRedirect = false;

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            Assert.IsNotNull(response.Headers["Location"], "Action " + testURL + " does not re-direct to Login");
                        }

                        break;
                }
            }
        }

        ///// <summary>
        ///// Logins the test.
        ///// </summary>
        ////[Test]
        ////public void LoginTest()
        ////{
        ////    create a new instance of WebClient
        ////    WebClient client = new WebClient();

        ////    set the user agent to IE6
        ////    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");

        ////    actually execute the GET request
        ////        string ret = client.DownloadString("http://localhost/Labinator2016/");
        ////    string token = MvcUtils.ExtractAntiForgeryToken(ret);
        ////    string cookies = client.ResponseHeaders["Set-Cookie"];
        ////    client.Headers.Add("Cookie", cookies);
        ////    System.Collections.Specialized.NameValueCollection formData = new System.Collections.Specialized.NameValueCollection();
        ////    formData["UserName"] = "paul.simpson@inin.com";
        ////    formData["Password"] = "password";
        ////    formData["__RequestVerificationToken"] = token;
        ////    formData["ReturnURL"] = "/Labinator2016/";
        ////    byte[] responseBytes = client.UploadValues("http://localhost/Labinator2016/Users/Login", "POST", formData);
        ////    string result = Encoding.UTF8.GetString(responseBytes);
        ////}
        ////        private AppHost appHost;

        ////[OneTimeSetUp]
        ////public void TestFixtureSetUp()
        ////{
        ////    //If you MVC project is not in the root of your solution directory then include the path
        ////    //e.g. AppHost.Simulate("Website\MyMvcApplication")
        ////    appHost = AppHost.Simulate("Labinator2016");
        ////}
        ////[Test]
        ////public void LogInProcess()
        ////{

        ////    appHost.Start(browsingSession =>
        ////    {
        ////        // First try to request a secured page without being logged in                
        ////        RequestResult initialRequestResult = browsingSession.Get("/");
        ////        Assert.IsNotNull(initialRequestResult.Response, "Didn't redirect.");
        ////        string loginRedirectUrl = initialRequestResult.Response.RedirectLocation;
        ////        Assert.IsTrue(loginRedirectUrl.StartsWith("/Users/Login"), "Didn't redirect to logon page");

        ////        //// Now follow redirection to logon page
        ////        //string loginFormResponseText = browsingSession.Get(loginRedirectUrl).ResponseText;
        ////        //string suppliedAntiForgeryToken = MvcUtils.ExtractAntiForgeryToken(loginFormResponseText);

        ////        //// Now post the login form, including the verification token
        ////        //RequestResult loginResult = browsingSession.Post(loginRedirectUrl, new
        ////        //{
        ////        //    UserName = "paul.simpson@inin.com",
        ////        //    Password = "password",
        ////        //    __RequestVerificationToken = suppliedAntiForgeryToken
        ////        //});
        ////        //string afterLoginRedirectUrl = loginResult.Response.RedirectLocation;
        ////        //Assert.AreEqual(securedActionUrl, afterLoginRedirectUrl, "Didn't redirect back to SecretAction");

        ////        //// Check that we can now follow the redirection back to the protected action, and are let in
        ////        //RequestResult afterLoginResult = browsingSession.Get(securedActionUrl);
        ////        //Assert.AreEqual("Hello, you're logged in as steve", afterLoginResult.ResponseText);
        ////    });
        ////}
    }
}
