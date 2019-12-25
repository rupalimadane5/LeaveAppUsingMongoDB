using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class UpdateLeaveTypeRequest
    {
        [Required]
        public string leavetype { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
