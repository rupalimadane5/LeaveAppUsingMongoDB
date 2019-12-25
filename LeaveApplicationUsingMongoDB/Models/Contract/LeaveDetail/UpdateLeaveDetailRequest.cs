using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class UpdateLeaveDetailRequest
    {
        [Required]
        public string userid { get; set; }
        [Required]
        public string leavetype { get; set; }
        [Required]
        public double leaveconsumed { get; set; }
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
