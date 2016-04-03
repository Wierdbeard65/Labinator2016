//-----------------------------------------------------------------------
// <copyright file="LabinatorService.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Labinator2016.Lib.Headers;
    using Labinator2016.Lib.Models;
    using Labinator2016.Lib.REST;

    /// <summary>
    /// The Service runs continually in the background and performs the timed and monitoring tasks.
    /// </summary>
    public class LabinatorService : System.ServiceProcess.ServiceBase
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabinatorService"/> class.
        /// </summary>
        /// This version is used when running.
        public LabinatorService()
        {
            this.ServiceName = "Labinator_Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            this.db = new LabinatorContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabinatorService" /> class.
        /// </summary>
        /// This version is used when Unit Testing.
        /// <param name="db">Handle to the dummy Database</param>
        public LabinatorService(ILabinatorDb db)
        {
            this.ServiceName = "Labinator_Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            this.db = db;
        }

        /// <summary>
        /// The StartStop timer fires off every 5 minutes. This handler simply passes the call through to the function that performs the work.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="args">The parameter is not used.</param>
        public void OnStartStopTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            this.StartStopTimer();
        }

        /// <summary>
        /// Function that performs the work of starting and stopping <see cref="Configuration"/>s / <see cref="Seat"/>s.
        /// </summary>
        /// This function begins by getting a list of <see cref="Classroom"/>s that should be running according to the schedule.
        /// Next it gets a list of all of the <see cref="Seat"/>s that are not part of a running <see cref="Classroom"/> before suspending the
        /// <see cref="VM"/>s.
        /// Next, it gets a list of the <see cref="Seat"/>s that are part of a running <see cref="Classroom"/> and starts them.
        public void StartStopTimer()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.startstopscancommenced, Detail = "Scan Started." });
            DateTime cutoff = DateTime.Now.AddMinutes(10);
            int suspendCount = 0;
            int startCount = 0;
            List<Course> courses = this.db.Query<Course>().ToList();
            List<Classroom> classrooms = this.db.Query<Classroom>().Where(c => (c.Start < cutoff)).ToList();
            List<int> classroomIds = classrooms.Join(
                courses,
                course => course.CourseId,
                classroom => classroom.CourseId,
                (classroom, course) => new
                {
                    ClassroomId = classroom.ClassroomId,
                    Start = classroom.Start,
                    Days = course.Days,
                    Hours = course.Hours
                }).Where(c => (c.Start.AddHours(c.Hours).TimeOfDay > cutoff.TimeOfDay) && (c.Start.AddDays(c.Days).Date > cutoff.Date)).Select(c => c.ClassroomId).ToList();

            List<Seat> seats = this.db.Query<Seat>().Where(s => !classroomIds.Contains(s.ClassroomId)).ToList();
            foreach (Seat s in seats)
            {
                List<SeatMachine> seatMachineList = this.db.Query<SeatMachine>().Where(sm => sm.SeatId == s.SeatId).ToList();
                List<Vm> vmlist = new List<Vm>();
                foreach (SeatMachine sm in seatMachineList)
                {
                    vmlist.Add(new Vm() { id = sm.VMId });
                }

                new Thread(Vm.SuspendMultiple).Start(vmlist);
                suspendCount++;
            }

            seats = seats = this.db.Query<Seat>().Where(s => classroomIds.Contains(s.ClassroomId)).ToList();
            startCount = 0;
            foreach (Seat s in seats)
            {
                List<SeatMachine> seatMachineList = this.db.Query<SeatMachine>().Where(sm => sm.SeatId == s.SeatId).ToList();
                List<Vm> vmlist = new List<Vm>();
                foreach (SeatMachine sm in seatMachineList)
                {
                    vmlist.Add(new Vm() { id = sm.VMId });
                }

                new Thread(Vm.StartMultiple).Start(vmlist);
                startCount++;
            }

            Log.Write(this.db, new Log() { Message = LogMessages.startstopscancomplete, Detail = "Scan finished." });
        }

        /// <summary>
        /// The Cleanup timer fires off every 24 hours. This handler simply passes the call through to the function that performs the work.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="args">The parameter is not used.</param>
        public void OnCleanupTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            this.CleanupTimer();
        }

        /// <summary>
        /// Function that performs the work of keeping the Database clean.
        /// </summary>
        /// The Cleanup process goes through the <see cref="SeatTemp"/> and <see cref="CourseMachineTemp"/> tables and removes any entries that are
        /// over 1 sday old. It then goes through the <see cref="Log"/> table and removes anything over 30 days old.
        public void CleanupTimer()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.cleanupscancommenced, Detail = "Scan Started." });
            DateTime cutoff = DateTime.Now.AddDays(-1);
            this.db.Delete<SeatTemp>(st => st.TimeStamp < cutoff);
            this.db.Delete<CourseMachineTemp>(cm => cm.TimeStamp < cutoff);
            cutoff = DateTime.Now.AddDays(-30);
            this.db.Delete<Log>(l => l.TimeStamp < cutoff);
            Log.Write(this.db, new Log() { Message = LogMessages.cleanupscancomplete, Detail = "Scan finished." });
        }

        /// <summary>
        /// Function to allow the service to be tested from the command line. Launches the service process and awaits a key press. The stops the service process
        /// </summary>
        /// <param name="args">Any arguments passed on the Command Line. This parameter is not used.</param>
        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        /// <summary>
        /// Starts the actual service process. Sets up and starts the various timers.
        /// </summary>
        /// <param name="args">This parameter is not used</param>
        protected override void OnStart(string[] args)
        {
            Log.Write(this.db, new Log() { Message = LogMessages.start, Detail = "Services starting." });
            //// Set up a startStopTimer to trigger every 5 minutes.
            System.Timers.Timer startStopTimer = new System.Timers.Timer();
            startStopTimer.Interval = 300000; // 5 Minutes
            startStopTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnStartStopTimer);
            startStopTimer.Start();
            System.Timers.Timer cleanupTimer = new System.Timers.Timer();
            cleanupTimer.Interval = 1000; // 5 Minutes
            cleanupTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnCleanupTimer);
            cleanupTimer.Start();
            ////System.Timers.Timer statusTimer = new System.Timers.Timer();
            ////statusTimer.Interval = 15000; // 15 Seconds
            ////statusTimer.Elapsed += new System.Timers.ElapsedEventHandler(RefreshStatuses.OnStatusTimer);
            ////statusTimer.Start();
            ////Log.Write(this.db, new Log() { Message = LogMessages.start, Detail = "Services started." });
            this.StartStopTimer();
            this.CleanupTimer();
            ////RefreshStatuses.StatusTimer();
        }

        /// <summary>
        /// Called when the Service is stopped. Performs cleanup.
        /// </summary>
        protected override void OnStop()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.stop, Detail = "Services stopped." });
        }

        /// <summary>
        /// Initializes the Service with a Name.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceName = "Labinator_Service";
        }
    }
}
