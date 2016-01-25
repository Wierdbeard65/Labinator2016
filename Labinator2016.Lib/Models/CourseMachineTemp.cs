using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labinator2016.Lib.Models
{
    public class CourseMachineTemp
    {
        public int CourseMachineTempId { get; set; }
        public string SessionId { get; set; }
        public string VMId { get; set; }
        public string VMName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }
    }
}
