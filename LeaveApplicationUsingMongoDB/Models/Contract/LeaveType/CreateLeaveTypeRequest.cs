using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class CreateLeaveTypeRequest
    {
        [Required]
        public string leavetype { get; set; }
    }
}
