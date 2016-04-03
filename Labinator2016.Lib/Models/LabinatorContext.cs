//-----------------------------------------------------------------------
// <copyright file="LabinatorContext.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// Version: 1.1 - Log Table Added
/// </summary>
namespace Labinator2016.Lib.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using EntityFramework.Extensions;
    using Labinator2016.Lib.Headers;

    /// <summary>
    /// Implements the Database interface for the actual database.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="Labinator2016.Lib.Headers.ILabinatorDb" />
    public partial class LabinatorContext : DbContext, ILabinatorDb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabinatorContext"/> class.
        /// </summary>
        public LabinatorContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LabinatorContext, Migrations.Configuration>("DefaultConnection"));
        }

        /// <summary>
        /// Gets or sets the Classrooms table reference.
        /// </summary>
        /// <value>
        /// A representation of the Classrooms table in the Database.
        /// </value>
        public DbSet<Classroom> Classrooms { get; set; }

        /// <summary>
        /// Gets or sets the Courses table reference.
        /// </summary>
        /// <value>
        /// A representation of the Courses table in the Database.
        /// </value>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Gets or sets the CourseMachines table reference.
        /// </summary>
        /// <value>
        /// A representation of the CourseMachines table in the Database.
        /// </value>
        public DbSet<CourseMachine> CourseMachines { get; set; }

        /// <summary>
        /// Gets or sets the temporary CourseMachines table reference.
        /// </summary>
        /// <value>
        /// A representation of the temporary CourseMachines table in the Database.
        /// </value>
        public DbSet<CourseMachineTemp> CourseMachineTemps { get; set; }

        /// <summary>
        /// Gets or sets the DataCenter table reference.
        /// </summary>
        /// <value>
        /// A representation of the DataCenter table in the Database.
        /// </value>
        public DbSet<DataCenter> DataCenters { get; set; }

        /// <summary>
        /// Gets or sets the logs table reference.
        /// </summary>
        /// <value>
        /// A representation of the Logs table in the Database.
        /// </value>
        public DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the seats table reference.
        /// </summary>
        /// <value>
        /// A representation of the Seats table in the Database.
        /// </value>
        public DbSet<Seat> Seats { get; set; }

        /// <summary>
        /// Gets or sets the Seat machines table reference.
        /// </summary>
        /// <value>
        /// A representation of the Seat Machines table in the Database.
        /// </value>
        public DbSet<SeatMachine> SeatMachines { get; set; }

        /// <summary>
        /// Gets or sets the seats table reference.
        /// </summary>
        /// <value>
        /// A representation of the Seats table in the Database.
        /// </value>
        public DbSet<SeatTemp> SeatTemps { get; set; }

        /// <summary>
        /// Gets or sets the Users table reference.
        /// </summary>
        /// <value>
        /// A representation of the Users table in the Database.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Implements the Interface Query on the Database.
        /// </summary>
        /// <typeparam name="T">Table being queried</typeparam>
        /// <returns>
        /// The data from the table.
        /// </returns>
        IQueryable<T> ILabinatorDb.Query<T>()
        {
            return this.Set<T>();
        }

        /// <summary>
        /// Implements the Delete Record Set (Part of Entity Framework Extended) on the database.
        /// </summary>
        /// <typeparam name="T1">Type (Table) to delete records from.</typeparam>
        /// <param name="target">A LINQ expression that specifies record set to be deleted</param>
        void ILabinatorDb.Delete<T1>(System.Linq.Expressions.Expression<Func<T1, bool>> target) 
        {
            this.Set<T1>().Where(target).Delete();
        }

        /// <summary>
        /// Implements the Add Record Interface on the Database.
        /// </summary>
        /// <typeparam name="T">Type (Table) to add record to.</typeparam>
        /// <param name="entity">Data to add.</param>
        void ILabinatorDb.Add<T>(T entity)
        {
            this.Set<T>().Add(entity);
        }

        /// <summary>
        /// Implements the Update Record Interface on the Database.
        /// </summary>
        /// <typeparam name="T">Type (Table) to add record to.</typeparam>
        /// <param name="entity">Data to update.</param>
        void ILabinatorDb.Update<T>(T entity)
        {
            this.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Implements the Remove a Record Interface on the Database.
        /// </summary>
        /// <typeparam name="T">Type (Table) to add record to.</typeparam>
        /// <param name="entity">Data to remove.</param>
        void ILabinatorDb.Remove<T>(T entity)
        {
            this.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Implements the Save Outstanding Changes Interface on the Database.
        /// </summary>
        void ILabinatorDb.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}