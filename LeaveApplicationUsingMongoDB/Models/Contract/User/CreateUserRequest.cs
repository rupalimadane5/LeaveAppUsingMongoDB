using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class CreateUserRequest
    {
        [Required]
        public string userid { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string designation { get; set; }
    }
}
