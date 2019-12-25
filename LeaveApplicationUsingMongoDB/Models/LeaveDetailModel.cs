using MongoDB.Bson;
using System;

namespace LeaveApplicationUsingMongoDB.Models
{
    public class LeaveDetailModel
    {
        public ObjectId _id { get; set; }
        public string userid { get; set; }
        public string leavetype { get; set; }
        public double leavecredit { get; set; }
        public double leaveconsumed { get; set; }
        public double leavebalance { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string status { get; set; }
        public bool isActive { get; set; }
    }
}
