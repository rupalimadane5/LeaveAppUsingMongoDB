using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class UpdateUserRequest
    {
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string designation { get; set; }
        public bool isActive { get; set; }
    }
}
