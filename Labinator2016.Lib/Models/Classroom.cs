namespace Labinator2016.Lib.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Classroom
    {
        public int ClassroomId { get; set; }

        public int CourseId { get; set; }

        public int DataCenterId { get; set; }

        public int UserId { get; set; }

        public string Project { get; set; }

        public DateTime Start { get; set; }

        public virtual Course Course { get; set; }

        public virtual DataCenter DataCenter { get; set; }

        [NotMapped]
        public string JsDate
        {
            get
            {
                return this.Start.ToShortDateString();
            }
        }
    }
}