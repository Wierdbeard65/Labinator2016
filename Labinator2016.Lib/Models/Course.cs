using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labinator2016.Lib.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public string Template { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }
        ////public virtual List<Classroom> Classrooms { get; set; }
        ////public virtual List<Machine> Machines { get; set; }
        ////public virtual List<Log> Logs { get; set; }
        [NotMapped]
        public string TemplateName { get; set; }
    }
}