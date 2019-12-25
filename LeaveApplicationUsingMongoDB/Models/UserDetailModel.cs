using MongoDB.Bson;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class UserDetailModel
    {
        public ObjectId _id { get; set; }
        public string userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string designation { get; set; }
        public bool isActive { get; set; }
    }
}
