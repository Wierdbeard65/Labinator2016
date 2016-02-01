using System.Collections.Generic;
using System.Linq;
using Labinator2016.Lib.Models;

namespace Labinator2016.Tests.TestData
{
    class TestCourseMachineData
    {
        public static IQueryable<CourseMachine> CourseMachines
        {
            get
            {
                var courseMachines = new List<CourseMachine>();
                for (int i = 1; i < 10; i++)
                {
                    var courseMachine = new CourseMachine();
                    courseMachine.CourseMachineId = i;
                    courseMachine.CourseId = 1;
                    courseMachine.IsActive = true;
                    courseMachine.VMId = "" + i + i + i + i + i + i + i + i + "";
                    courseMachines.Add(courseMachine);
                }
                for (int i = 1; i < 10; i++)
                {
                    var courseMachine = new CourseMachine();
                    courseMachine.CourseMachineId = i;
                    courseMachine.CourseId = 2;
                    courseMachine.IsActive = true;
                    courseMachine.VMId = "" + i + i + i + i + i + i + i + i + "";
                    courseMachines.Add(courseMachine);
                }
                return courseMachines.AsQueryable();
            }
        }
    }
}
