using MongoDB.Bson;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class LeaveTypeModel
    {
        public ObjectId _id { get; set; }
        public string leavetype { get; set; }
        public bool isActive { get; set; }
    }
}
