using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeaveApplicationUsingMongoDB.Models;
using LeaveApplicationUsingMongoDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveApplicationUsingMongoDB.Controllers
{
    [Route("api/leave/detail")]
    [ApiController]
    public class LeaveDetailController : ControllerBase
    {
        LeaveDetailsRepository _leaveDetailsRepository;
        public LeaveDetailController()
        {
            _leaveDetailsRepository = new LeaveDetailsRepository();
        }

        [HttpGet("{userid}")]
        public ActionResult<List<LeaveDetailModel>> Get([Required]string userid, [FromQuery]string leavetype, [FromQuery]string status)
        {
            try
            {
                var leaveDetailsModel = _leaveDetailsRepository.GetLeaveDetailsByUserId(userid, leaveType: leavetype, status: status);
                if (leaveDetailsModel == null || leaveDetailsModel.Count <= 0)
                {
                    //throw new Exception($"Leave details not found");
                    return null;
                }

                return leaveDetailsModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leave details {ex.Message}");
            }

        }

        [HttpPost]
        public ActionResult<LeaveDetailModel> CreateLeaveDetail([Required]CreateLeaveDetailRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveDetailsModel = _leaveDetailsRepository.GetLeaveDetailsByUserId(request.userid, leaveType: request.leavetype);
                if (leaveDetailsModel != null && leaveDetailsModel.Count > 0)
                {
                    //throw new Exception($"Leave detail already present for LeaveType {request.leavetype}");
                    return null;
                }

                return _leaveDetailsRepository.CreateLeaveDetail(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create leave balance {ex.Message}");
            }

        }

        [HttpPut]
        public ActionResult<LeaveDetailModel> UpdateLeaveDetail([Required]UpdateLeaveDetailRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveDetailsModel = _leaveDetailsRepository.GetLeaveDetailsByUserId(request.userid, leaveType: request.leavetype);
                if (leaveDetailsModel == null)
                {
                    //throw new Exception($"Leave details not present for LeaveType {request.leavetype}");
                    return null;
                }

                return _leaveDetailsRepository.UpdateLeaveDetail(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update leave balance details {ex.Message}");
            }

        }

        [HttpDelete("{userid}/{leavetype}")]
        public ActionResult<bool> DeleteLeaveDetail([Required] string userid, [Required] string leavetype)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveDetailsModel = _leaveDetailsRepository.GetLeaveDetailsByUserId(userid, leaveType: leavetype);
                if (leaveDetailsModel == null)
                {
                    //throw new Exception($"Leave details not present for LeaveType {leaveType}");
                    return null;
                }

                return _leaveDetailsRepository.DeleteLeaveDetail(userid, leavetype);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete leave details {ex.Message}");
            }

        }

    }
}