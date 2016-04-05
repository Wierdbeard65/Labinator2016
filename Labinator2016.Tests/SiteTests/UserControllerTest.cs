//-----------------------------------------------------------------------
// <copyright file="UserControllerTest.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Tests.Controllers
{
    using System.Web.Mvc;
    using Labinator2016.Controllers;
    using Labinator2016.ViewModels;
    using Labinator2016.Tests.TestData;
    using Lib.Models;
    using NUnit.Framework;
    using Lib.Utilities;
    using Lib.Headers;
    [TestFixture]
    public class UserControllerTest
    {
        [Test]
        public void UsersControllerInitialIndexTest()
        {
            // Arrange
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            UsersController controller = new UsersController(db, auth);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void UsersControllerEditNewUserInitialTest()
        {
            // Arrange
            UsersController controller = new UsersController();
            
            // Act
            ViewResult result = controller.Edit(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New", ((User)result.Model).EmailAddress);
            Assert.AreEqual(0, ((User)result.Model).UserId);
        }
        [Test]
        public void UsersControllerEditExistingUserInitialTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
////            var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
////            db.AddSet(TestCourseData.Courses);
////            db.AddSet(TestClassroomData.Classrooms);
////            st.AddSet(TestTemplateRESTData.templates);
            var controller = new UsersController(db, auth);
////            controller.ControllerContext = new FakeControllerContext();
            ViewResult result = controller.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(User), result.Model.GetType());
            Assert.AreEqual("testuser1@test.com", ((User)result.Model).EmailAddress);
        }
        [Test]
        public void UsersControllerEditNewUserPostbackValidPasswordTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////db.AddSet(TestCourseData.Courses);
            ////db.AddSet(TestClassroomData.Classrooms);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 0, EmailAddress = "TestNew", NewPassword1 = "TestPassword", NewPassword2 = "TestPassword" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(1, db.Added.Count);
            Assert.AreEqual("TestNew", ((User)db.Added[0]).EmailAddress);
            Assert.AreEqual(true, PasswordHash.ValidatePassword("TestPassword", ((User)db.Added[0]).Password));
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(2, db.saved);
        }
        [Test]
        public void UsersControllerEditNewUserPostbackInValidPasswordTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////db.AddSet(TestCourseData.Courses);
            ////db.AddSet(TestClassroomData.Classrooms);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 0, EmailAddress = "TestNew", NewPassword1 = "TestPassword", NewPassword2 = "WrongPassword" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
            Assert.AreEqual(0, db.Added.Count);
        }

        [Test]
        public void UsersControllerEditExistingUserPostbackAdminsitratorNoPasswordChangeTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 1, EmailAddress = "TestUpdate" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(0, db.Added.Count);
            Assert.AreEqual(1, db.Updated.Count);
            Assert.AreEqual("TestUpdate", ((User)db.Updated[0]).EmailAddress);
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(2, db.saved);
        }
        [Test]
        public void UsersControllerEditExistingUserPostbackUserValidPasswordChangeTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 1, EmailAddress = "TestUpdate", OldPassword= "password", NewPassword1="NewPassword", NewPassword2="NewPassword" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual(0, db.Added.Count);
            Assert.AreEqual(1, db.Updated.Count);
            Assert.AreEqual("TestUpdate", ((User)db.Updated[0]).EmailAddress);
            Assert.AreEqual(true, PasswordHash.ValidatePassword("NewPassword", ((User)db.Updated[0]).Password));
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(2, db.saved);
        }
        [Test]
        public void UsersControllerEditExistingUserPostbackUserInvalidPasswordChangeBadOldPasswordTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 1, EmailAddress = "TestUpdate", OldPassword = "wrong", NewPassword1 = "NewPassword", NewPassword2 = "NewPassword" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
            Assert.AreEqual(0, db.Added.Count);
        }
        [Test]
        public void UsersControllerEditExistingUserPostbackUserInvalidPasswordChangeBadNewPasswordTest()
        {
            var db = new FakeDatabase();
            IAuth auth = new FakeFormAuthenticator();
            ////var st = new FakeSkyTap();
            db.AddSet(TestUserData.Users);
            ////st.AddSet(TestTemplateRESTData.templates);
            UsersController controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            User testUser = new User() { UserId = 1, EmailAddress = "TestUpdate", OldPassword = "password", NewPassword1 = "NewPassword", NewPassword2 = "WrongPassword" };
            var result = controller.Edit(testUser);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(typeof(RedirectToRouteResult), result.GetType());
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
            Assert.AreEqual(0, db.Added.Count);
        }
        [Test]
        public void ValidUser()
        {
            var db = new FakeDatabase();
            var auth = new FakeFormAuthenticator();
            db.AddSet(TestUserData.Users);
            var controller = new UsersController(db, auth);
            controller.ControllerContext = new FakeControllerContext();
            var result = controller.Login(new LoginViewModel() { UserName = "testuser0@test.com", Password = "password", ReturnUrl = "/" });
            Assert.AreEqual(typeof(RedirectResult), result.GetType());
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(LogMessages.logon, ((Log)db.LogAdded[0]).Message);
        }
        [Test]
        public void InValidUser()
        {
            var db = new FakeDatabase();
            var auth = new FakeFormAuthenticator();
            db.AddSet(TestUserData.Users);
            var cx = new FakeControllerContext();
            var controller = new UsersController(db, auth);
            controller.ControllerContext = cx;
            var result = controller.Login(new LoginViewModel() { UserName = "NonExistent@test.com", Password = "password", ReturnUrl = "/" }) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.ModelState[""]);
            Assert.IsNotNull(result.ViewData.ModelState[""].Errors);
            Assert.IsTrue(result.ViewData.ModelState[""].Errors.Count == 1);
            Assert.AreEqual("The User Name or password provided is incorrect.", result.ViewData.ModelState[""].Errors[0].ErrorMessage);
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(LogMessages.incorrectlogon, ((Log)db.LogAdded[0]).Message);
        }
        [Test]
        public void InvalidPassword()
        {
            var db = new FakeDatabase();
            var auth = new FakeFormAuthenticator();
            db.AddSet(TestUserData.Users);
            var cx = new FakeControllerContext();
            var controller = new UsersController(db, auth);
            controller.ControllerContext = cx;
            var result = controller.Login(new LoginViewModel() { UserName = "TestUser0@test.com", Password = "bad", ReturnUrl = "/" }) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.ModelState[""]);
            Assert.IsNotNull(result.ViewData.ModelState[""].Errors);
            Assert.IsTrue(result.ViewData.ModelState[""].Errors.Count == 1);
            Assert.AreEqual("The User Name or password provided is incorrect.", result.ViewData.ModelState[""].Errors[0].ErrorMessage);
            Assert.AreEqual(1, db.LogAdded.Count);
            Assert.AreEqual(LogMessages.incorrectlogon, ((Log)db.LogAdded[0]).Message);
        }

    }
}
