using LeaveApplicationUsingMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace LeaveApplicationUsingMongoDB.Repository
{
    public class LeaveTypeRepository
    {
        MongoClient client = null;
        IMongoDatabase database = null;
        IMongoCollection<LeaveTypeModel> leaveTypeCollection = null;
        public LeaveTypeRepository()
        {
            // connect to mongo db via a mongo client
            client = new MongoClient("mongodb://localhost:27017");

            // get reference of the required database
            database = client.GetDatabase("leaveDB");

            leaveTypeCollection = database.GetCollection<LeaveTypeModel>("leavetype");
        }

        public LeaveTypeModel GetLeaveTypes(string leaveType)
        {
            return leaveTypeCollection.Find(x => x.leavetype.Equals(leaveType)).FirstOrDefault();
        }

        public List<LeaveTypeModel> GetLeaveTypes()
        {
            return leaveTypeCollection.AsQueryable().ToList();
        }

        public LeaveTypeModel CreateLeaveType(CreateLeaveTypeRequest request)
        {
            var leaveTypeModel = new LeaveTypeModel()
            {
                leavetype = request.leavetype,
                isActive = true
            };

            leaveTypeCollection.InsertOne(leaveTypeModel);

            return GetLeaveTypes(request.leavetype);
        }

        public LeaveTypeModel UpdateLeaveType(string leaveType,UpdateLeaveTypeRequest request)
        {
            var result = leaveTypeCollection.UpdateOne(
                Builders<LeaveTypeModel>.Filter.Eq("leavetype", leaveType),
                Builders<LeaveTypeModel>.Update.Set("leavetype", request.leavetype)
                                               .Set("isActive", request.isActive)
                );
            return result.ModifiedCount > 0 ? GetLeaveTypes(request.leavetype) : null;
        }

        public bool DeleteLeaveType(string leaveType)
        {
            var result = leaveTypeCollection.DeleteOne(
                Builders<LeaveTypeModel>.Filter.Eq("leavetype", leaveType)
                );

            return result.DeletedCount > 0 ? true : false;
        }
    }
}
