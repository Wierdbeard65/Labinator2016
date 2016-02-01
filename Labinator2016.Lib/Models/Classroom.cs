namespace Labinator2016.Lib.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Classroom
    {
        public Int32 ClassroomId { get; set; }
        public Int32 CourseId { get; set; }
        public Int32 DataCenterId { get; set; }
        public Int32 UserId { get; set; }
        public string Project { get; set; }
        public DateTime Start { get; set; }
    }
}