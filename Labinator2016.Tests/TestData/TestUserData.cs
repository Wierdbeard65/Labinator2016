namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Labinator2016.Lib.Models;
    using Labinator2016.Lib.Utilities;

    class TestUserData
    {
        public static IQueryable<User> Users
        {
            get
            {
                var users = new List<User>();
                for (int i = 0; i < 100; i++)
                {
                    var user = new User();
                    user.UserId = i;
                    user.EmailAddress = "testuser" + i + "@test.com";
                    user.Password = PasswordHash.CreateHash("password");
                    user.IsAdministrator = (i / 5 == Math.Ceiling((double)i / 5));
                    user.IsInstructor = (i / 3 == Math.Ceiling((double)i / 3));
                    users.Add(user);
                }
                return users.AsQueryable();
            }
        }
    }
}
