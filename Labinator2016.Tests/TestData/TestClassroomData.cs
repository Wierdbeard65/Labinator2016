namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Labinator2016.Lib.Models;
    class TestClassroomData
    {
        public static IQueryable<Classroom> Classrooms
        {
            get
            {
                var classrooms = new List<Classroom>();
                for (int i = 0; i < 5; i++)
                {
                    var classroom = new Classroom();
                    classroom.ClassroomId = i;
                    classroom.CourseId = i;
                    classroom.DataCenterId = i;
                    classroom.Project = "Test" + i;
                    classroom.UserId = i;
                    classrooms.Add(classroom);
                }
                return classrooms.AsQueryable();
            }
        }
    }
}