using MongoDB.Bson;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class LeaveBalanceModel
    {
        public ObjectId _id { get; set; }
        public string leavetype { get; set; }
        public double leavecredit { get; set; }
        public string year { get; set; }
        public bool isActive { get; set; }
    }
}
