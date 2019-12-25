using LeaveApplicationUsingMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace LeaveApplicationUsingMongoDB.Repository
{
    public class LeaveBalanceRepository
    {
        MongoClient client = null;
        IMongoDatabase database = null;
        IMongoCollection<LeaveBalanceModel> leaveBalanceCollection = null;
        public LeaveBalanceRepository()
        {
            // connect to mongo db via a mongo client
            client = new MongoClient("mongodb://localhost:27017");

            // get reference of the required database
            database = client.GetDatabase("leaveDB");

            leaveBalanceCollection = database.GetCollection<LeaveBalanceModel>("leavebalance");
        }

        public List<LeaveBalanceModel> GetAllLeaveBalance()
        {
            return leaveBalanceCollection.AsQueryable().ToList();
        }

        public LeaveBalanceModel GetLeaveBalanceByType(string leaveType)
        {
            return leaveBalanceCollection.Find(x => x.leavetype.Equals(leaveType)).FirstOrDefault();
        }

        public LeaveBalanceModel CreateLeaveBalance(CreateLeaveBalanceRequest request)
        {
            var leaveBalanceModel = new LeaveBalanceModel()
            {
                leavetype = request.leavetype,
                leavecredit = request.leavecredit,
                year = request.year,
                isActive = true
            };

            leaveBalanceCollection.InsertOne(leaveBalanceModel);

            return GetLeaveBalanceByType(request.leavetype);
        }

        public LeaveBalanceModel UpdateLeaveBalance(string leaveType, UpdateLeaveBalanceRequest request)
        {
            var result = leaveBalanceCollection.UpdateOne(
               Builders<LeaveBalanceModel>.Filter.Eq("leavetype", leaveType),
               Builders<LeaveBalanceModel>.Update.Set("leavecredit", request.leavecredit)
                                                 .Set("year", request.year)
                                                 .Set("isActive", request.isActive)
               );
            return result.ModifiedCount > 0 ? GetLeaveBalanceByType(leaveType) : null;
        }

        public bool DeleteLeaveBalance(string leaveType)
        {
            var result = leaveBalanceCollection.DeleteOne(
                Builders<LeaveBalanceModel>.Filter.Eq("leavetype", leaveType)
                );

            return result.DeletedCount > 0 ? true : false;
        }
    }
}
