namespace Labinator2016.Tests.TestData
{
    using System.Collections.Generic;
    using System.Linq;
    using Labinator2016.Lib.Models;

    class TestCourseData
    {
        public static IQueryable<Course> Courses
        {
            get
            {
                var courses = new List<Course>();
                for (int i = 1; i < 100; i++)
                {
                    var course = new Course();
                    course.CourseId = i;
                    course.Name = "Test" + i;
                    courses.Add(course);
                }
                return courses.AsQueryable();
            }
        }
    }
}