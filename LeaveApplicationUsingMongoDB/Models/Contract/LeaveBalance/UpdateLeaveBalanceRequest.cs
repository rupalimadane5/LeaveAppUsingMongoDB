using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class UpdateLeaveBalanceRequest
    {
        [Required]
        [Range(0.5, 10)]
        public double leavecredit { get; set; }
        [Required]
        public string year { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
