using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class CreateLeaveDetailRequest
    {
        [Required]
        public string userid { get; set; }
        [Required]
        public string leavetype { get; set; }
        [Required]
        public double leavecredit { get; set; }
        [Required]
        public double leaveconsumed { get; set; }
        [Required]
        public double leavebalance { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        [Required]
        public DateTime enddate { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
