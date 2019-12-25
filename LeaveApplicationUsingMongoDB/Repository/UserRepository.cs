using LeaveApplicationUsingMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LeaveApplicationUsingMongoDB.Repository
{
    public class UserRepository
    {
        MongoClient client = null;
        IMongoDatabase database = null;
        IMongoCollection<UserDetailModel> userCollection = null;
        public UserRepository()
        {
            // connect to mongo db via a mongo client
            client = new MongoClient("mongodb://localhost:27017");

            // get reference of the required database
            database = client.GetDatabase("leaveDB");

            userCollection = database.GetCollection<UserDetailModel>("userdetail");
        }
        public List<UserDetailModel> GetAllUser()
        {
            return userCollection.AsQueryable().ToList();
        }

        public UserDetailModel GetUserById(string userId)
        {
            var query = userCollection.Find<UserDetailModel>(x => x.userid.Equals(userId));
            return query.FirstOrDefault();
        }

        public UserDetailModel CreateUser(CreateUserRequest request)
        {
            var userModel = new UserDetailModel()
            {
                userid = request.userid,
                firstname = request.firstname,
                lastname = request.lastname,
                designation = request.designation,
                isActive = true
            };

            userCollection.InsertOne(userModel);

            return GetUserById(request.userid);
        }

        public UserDetailModel UpdateUser(string userId, UpdateUserRequest request)
        {
            var result = userCollection.UpdateOne(
                Builders<UserDetailModel>.Filter.Eq("userid", userId),
                Builders<UserDetailModel>.Update.Set("firstname", request.firstname)
                                                 .Set("lastname", request.lastname)
                                                 .Set("designation", request.designation)
                                                 .Set("isActive", request.isActive)
                );
            return result.ModifiedCount > 0 ? GetUserById(userId) : null;
        }

        public bool DeleteUser(string userId)
        {
            var result = userCollection.DeleteOne(
                Builders<UserDetailModel>.Filter.Eq("userid", userId)
                );

            return result.DeletedCount > 0 ? true : false;
        }
    }
}
