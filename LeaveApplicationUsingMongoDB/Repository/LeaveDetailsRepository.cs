using LeaveApplicationUsingMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace LeaveApplicationUsingMongoDB.Repository
{
    public class LeaveDetailsRepository
    {
        MongoClient client = null;
        IMongoDatabase database = null;
        IMongoCollection<LeaveDetailModel> leaveDetailsCollection = null;
        public LeaveDetailsRepository()
        {
            // connect to mongo db via a mongo client
            client = new MongoClient("mongodb://localhost:27017");

            // get reference of the required database
            database = client.GetDatabase("leaveDB");

            leaveDetailsCollection = database.GetCollection<LeaveDetailModel>("leavedetail");
        }

        public List<LeaveDetailModel> GetLeaveDetailsByUserId(string userid, string leaveType = null, string status = null)
        {
            if (leaveType != null && status == null)
            {
                return leaveDetailsCollection.Find(x => x.userid.Equals(userid) && x.leavetype.Equals(leaveType)).ToList();
            }
            else if (leaveType == null && status != null)
            {
                return leaveDetailsCollection.Find(x => x.userid.Equals(userid) && x.status.Equals(status)).ToList();
            }
            else if (leaveType != null && status != null)
            {
                return leaveDetailsCollection.Find(x => x.userid.Equals(userid) && x.leavetype.Equals(leaveType) && x.status.Equals(status)).ToList();
            }

            return leaveDetailsCollection.Find(x => x.userid.Equals(userid)).ToList();
        }

        public LeaveDetailModel CreateLeaveDetail(CreateLeaveDetailRequest request)
        {
            var leaveDetailModel = new LeaveDetailModel()
            {
                userid = request.userid,
                leavetype = request.leavetype,
                leavecredit = request.leavecredit,
                leaveconsumed = request.leaveconsumed,
                leavebalance = request.leavebalance,
                startdate = request.startdate,
                enddate = request.enddate,
                status = request.status,
                isActive = true
            };

            leaveDetailsCollection.InsertOne(leaveDetailModel);
            return GetLeaveDetailsByUserId(request.userid, leaveType: request.leavetype).FirstOrDefault();

        }

        public LeaveDetailModel UpdateLeaveDetail(UpdateLeaveDetailRequest request)
        {
            var leavedetail = GetLeaveDetailsByUserId(request.userid, leaveType: request.leavetype).FirstOrDefault();

            var filter = Builders<LeaveDetailModel>.Filter.Eq("leavetype", request.leavetype);
            filter = filter & Builders<LeaveDetailModel>.Filter.Eq("userid", request.userid);

            var result = leaveDetailsCollection.UpdateOne(
              filter,
              Builders<LeaveDetailModel>.Update.Set("leaveconsumed", leavedetail.leaveconsumed + request.leaveconsumed)
                                               .Set("leavebalance", leavedetail.leavebalance - request.leaveconsumed)
                                               .Set("startdate", request.startdate)
                                               .Set("enddate", request.enddate)
                                               .Set("status", request.status)
                                               .Set("isActive", request.isActive)
              );
            return result.ModifiedCount > 0 ? GetLeaveDetailsByUserId(request.userid, leaveType: request.leavetype).FirstOrDefault() : null;
        }

        public bool DeleteLeaveDetail(string userId, string leaveType)
        {
            var filter = Builders<LeaveDetailModel>.Filter.Eq("leavetype", leaveType);
            filter = filter & Builders<LeaveDetailModel>.Filter.Eq("userid", userId);

            var result = leaveDetailsCollection.DeleteOne(filter);

            return result.DeletedCount > 0 ? true : false;
        }
    }
}
