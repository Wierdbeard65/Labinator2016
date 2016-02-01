using System;
using System.Collections.Generic;
using System.Linq;
using Labinator2016.Lib.Models;

namespace Labinator2016.Tests.TestData
{
    class TestCourseMachineTempData
    {
        public static IQueryable<CourseMachineTemp> CourseMachineTemps
        {
            get
            {
                var courseMachineTemps = new List<CourseMachineTemp>();
                for (int i = 1; i < 10; i++)
                {
                    var courseMachineTemp = new CourseMachineTemp();
                    courseMachineTemp.CourseMachineTempId = i;
                    courseMachineTemp.IsActive = true;
                    courseMachineTemp.SessionId = "12345";
                    courseMachineTemp.VMName = "Test" + i;
                    courseMachineTemp.VMId = "" + i + i + i + i + i + i + i + i + "";
                    courseMachineTemp.TimeStamp = DateTime.Now;
                    courseMachineTemps.Add(courseMachineTemp);
                }
                for (int i = 1; i < 10; i++)
                {
                    var courseMachineTemp = new CourseMachineTemp();
                    courseMachineTemp.CourseMachineTempId = i;
                    courseMachineTemp.IsActive = true;
                    courseMachineTemp.SessionId = "OtherTest";
                    courseMachineTemp.VMName = "Test" + i;
                    courseMachineTemp.VMId = "" + i + i + i + i + i + i + i + i + "";
                    courseMachineTemp.TimeStamp = DateTime.Now;
                    courseMachineTemps.Add(courseMachineTemp);
                }
                return courseMachineTemps.AsQueryable();
            }
        }
    }
}
