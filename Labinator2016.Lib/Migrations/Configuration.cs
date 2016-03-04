//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
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

    /// <summary>
    /// Database context.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{Labinator2016.Lib.Models.LabinatorContext}" />
    internal sealed class Configuration : DbMigrationsConfiguration<Labinator2016.Lib.Models.LabinatorContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param Name="context">Context to be used for updating seed data.</param>
        /// <remarks>
        /// Note that the database may already contain seed data when this method runs. This means that
        /// implementations of this method must check whether or not seed data is present and/or up-to-date
        /// and then only make changes if necessary and in a non-destructive way. The
        /// <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])" />
        /// can be used to help with this, but for seeding large amounts of data it may be necessary to do less
        /// granular checks if performance is an issue.
        /// If the <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2" /> database
        /// initializer is being used, then this method will be called each time that the initializer runs.
        /// If one of the <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1" />, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1" />,
        /// or <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1" /> initializers is being used, then this method will not be
        /// called and the Seed method defined in the initializer should be used instead.
        /// </remarks>
        protected override void Seed(Labinator2016.Lib.Models.LabinatorContext context)
        {
            string password = PasswordHash.CreateHash("password");
            List<User> users = new List<User>
            {
                new User() { EmailAddress = "system", IsAdministrator = true, IsInstructor = true, Password = password },
                new User() { EmailAddress = "paul.simpson@inin.com", IsAdministrator = true, IsInstructor = true, Password = password, STAPIKey = "c4246ea631bf8a6c97d85997a3494082251ece3e" }
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
