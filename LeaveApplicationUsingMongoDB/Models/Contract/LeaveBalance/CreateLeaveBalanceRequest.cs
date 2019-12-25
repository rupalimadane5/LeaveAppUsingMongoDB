using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class CreateLeaveBalanceRequest
    {
        [Required]
        public string leavetype { get; set; }
        [Required]
        [Range(0.5, 10)]
        public double leavecredit { get; set; }
        [Required]
        public string year { get; set; }
    }
}
