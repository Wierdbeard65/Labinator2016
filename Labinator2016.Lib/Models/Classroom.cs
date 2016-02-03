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
        public int ClassroomId { get; set; }
        public int CourseId { get; set; }
        public int DataCenterId { get; set; }
        public int UserId { get; set; }
        public string Project { get; set; }
        public DateTime Start { get; set; }
        public virtual Course course { get; set; }
        public virtual DataCenter dataCenter { get; set; }
    }
}