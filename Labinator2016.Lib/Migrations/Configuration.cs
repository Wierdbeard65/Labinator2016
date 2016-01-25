namespace Labinator2016.Lib.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Headers;
    using Models;
    using Utilities;
    internal sealed class Configuration : DbMigrationsConfiguration<Labinator2016.Lib.Models.LabinatorContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Labinator2016.Lib.Models.LabinatorContext context)
        {
            String password = PasswordHash.CreateHash("password");
            List<User> users = new List<User> {
                new User() { EmailAddress = "system", IsAdministrator = true, IsInstructor = true, Password = password },
                new User() { EmailAddress = "paul.simpson@inin.com", IsAdministrator = true, IsInstructor = true, Password = password, STAPIKey="c4246ea631bf8a6c97d85997a3494082251ece3e" }
            };
            context.Users.AddOrUpdate(u => u.EmailAddress, users.ToArray());
            context.SaveChanges();
            ////List<Log> logs = new List<Log> {
            ////    new Log() {Message=LogMessages.create,Detail="Database Created" }
            ////};
            ////context.Logs.AddOrUpdate(l => l.Detail, logs.ToArray());
            ////context.SaveChanges();
        }
    }
}
