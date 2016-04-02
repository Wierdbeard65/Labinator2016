using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Labinator2016.Lib.Headers;
using Labinator2016.Lib.Models;
using Labinator2016.Lib.REST;

namespace Labinator2016.Service
{
    class LabinatorService : System.ServiceProcess.ServiceBase
    {
        /// <summary>
        /// Handle to the database
        /// </summary>
        private ILabinatorDb db;

        public LabinatorService()
        {
            this.ServiceName = "Labinator_Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            this.db = new LabinatorContext();
        }

        public LabinatorService(ILabinatorDb db)
        {
            this.ServiceName = "Labinator_Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            this.db = db;
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
        protected override void OnStart(string[] args)
        {
            Log.Write(this.db, new Log() { Message = LogMessages.start, Detail = "Services starting." });
            // Set up a startStopTimer to trigger every 5 minutes.
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
        protected override void OnStop()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.stop, Detail = "Services stopped." });
        }
        private void InitializeComponent()
        {
            // 
            // SkytapinatorService
            // 
            this.ServiceName = "Labinator_Service";
        }
        public void OnStartStopTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            this.StartStopTimer();
        }
        public void StartStopTimer()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.startstopscancommenced, Detail = "Scan Started." });
            DateTime cutoff = DateTime.Now.AddMinutes(10);
            int suspendCount = 0;
            int startCount = 0;
            List<Course> courses = this.db.Query<Course>().ToList();
            List<Classroom> classrooms = this.db.Query<Classroom>().Where(c=>(c.Start< cutoff)).ToList();
            List<int> classroomIds = classrooms.Join(
                courses,
                course=>course.CourseId,
                classroom=>classroom.CourseId,
                (classroom,course) => new
                {
                    ClassroomId = classroom.ClassroomId,
                    Start = classroom.Start,
                    Days = course.Days,
                    Hours = course.Hours
                }).Where(c=>(c.Start.AddHours(c.Hours).TimeOfDay>cutoff.TimeOfDay)&&(c.Start.AddDays(c.Days).Date>cutoff.Date)).Select(c=>c.ClassroomId).ToList();

            List<Seat> seats = this.db.Query<Seat>().Where(s=>!classroomIds.Contains(s.ClassroomId)).ToList();
            foreach (Seat s in seats)
            {
                List<SeatMachine> SMList = db.Query<SeatMachine>().Where(sm => sm.SeatId==s.SeatId).ToList();
                List<Vm> VMlist = new List<Vm>();
                foreach(SeatMachine sm in SMList)
                {
                    VMlist.Add(new Vm() { id = sm.VMId });
                }
                new Thread(Vm.SuspendMultiple).Start(VMlist);
                suspendCount++;
            }
            seats = seats = this.db.Query<Seat>().Where(s => classroomIds.Contains(s.ClassroomId)).ToList();
            startCount = 0;
            foreach (Seat s in seats)
            {
                List<SeatMachine> SMList = db.Query<SeatMachine>().Where(sm => sm.SeatId == s.SeatId).ToList();
                List<Vm> VMlist = new List<Vm>();
                foreach (SeatMachine sm in SMList)
                {
                    VMlist.Add(new Vm() { id = sm.VMId });
                }
                new Thread(Vm.StartMultiple).Start(VMlist);
                startCount++;
            }
            Log.Write(this.db, new Log() { Message = LogMessages.startstopscancomplete, Detail = "Scan finished." });
        }
        public void OnCleanupTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            this.CleanupTimer();
        }
        public void CleanupTimer()
        {
            Log.Write(this.db, new Log() { Message = LogMessages.cleanupscancommenced, Detail = "Scan Started." });
            DateTime cutoff = DateTime.Now.AddDays(-1);
            this.db.Delete<SeatTemp>(st => st.TimeStamp < cutoff);
            this.db.Delete<CourseMachineTemp>(cm => cm.TimeStamp < cutoff);
            cutoff = DateTime.Now.AddDays(-30);
            this.db.Delete<Log>(l=>l.TimeStamp < cutoff);
            Log.Write(this.db, new Log() { Message = LogMessages.cleanupscancomplete, Detail = "Scan finished." });
        }
    }
}
