using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LeaveApplicationUsingMongoDB.Models;
using LeaveApplicationUsingMongoDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LeaveApplicationUsingMongoDB.Controllers
{
    [Route("api/leave/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _userRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpGet]
        public ActionResult<List<UserDetailModel>> Get()
        {
            try
            {
                var userModel = _userRepository.GetAllUser();
                if (userModel == null)
                {
                    //throw new Exception($"User details not found");
                    return null;
                }

                return userModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get user details {ex.Message}");
            }

        }

        [HttpGet("{userid}")]
        public ActionResult<UserDetailModel> Get([Required]string userid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var userModel = _userRepository.GetUserById(userid);
                if (userModel == null)
                {
                    //throw new Exception($"User details not found for UserId {userid}");
                    return null;
                }

                return userModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get user details {ex.Message}");
            }

        }

        [HttpPost]
        public ActionResult<UserDetailModel> CreateUser([Required]CreateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var userModel = _userRepository.GetUserById(request.userid);
                if (userModel != null)
                {
                    //throw new Exception($"User already present for UserId {request.userid}");
                    return null;
                }

                return _userRepository.CreateUser(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user {ex.Message}");
            }

        }

        [HttpPut("{userid}")]
        public ActionResult<UserDetailModel> UpdateUser([Required] string userid, [Required]UpdateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var userModel = _userRepository.GetUserById(userid);
                if (userModel == null)
                {
                    //throw new Exception($"User details not present for UserId {userid}");
                    return null;
                }

                return _userRepository.UpdateUser(userid, request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update user details {ex.Message}");
            }

        }

        [HttpDelete("{userid}")]
        public ActionResult<bool> DeleteUser([Required] string userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var userModel = _userRepository.GetUserById(userId);
                if (userModel == null)
                {
                    //throw new Exception($"User details present for UserId {userId}");
                    return null;
                }

                return _userRepository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete user details {ex.Message}");
            }

        }
    }
}