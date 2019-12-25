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
    [Route("api/leave/balance")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        LeaveBalanceRepository _leaveBalanceRepository;
        public LeaveBalanceController()
        {
            _leaveBalanceRepository = new LeaveBalanceRepository();
        }

        [HttpGet]
        public ActionResult<List<LeaveBalanceModel>> Get()
        {
            try
            {
                var leaveBalanceModel = _leaveBalanceRepository.GetAllLeaveBalance();
                if (leaveBalanceModel == null)
                {
                    //throw new Exception($"Leave balance details not found ");
                    return null;
                }

                return leaveBalanceModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leave balance details {ex.Message}");
            }

        }

        [HttpGet("{leaveType}")]
        public ActionResult<LeaveBalanceModel> Get([Required]string leaveType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveBalanceModel = _leaveBalanceRepository.GetLeaveBalanceByType(leaveType);
                if (leaveBalanceModel == null)
                {
                    //throw new Exception($"Leave balance details not found for LeaveType {leaveType}");
                    return null;
                }

                return leaveBalanceModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leave balance details {ex.Message}");
            }

        }

        [HttpPost]
        public ActionResult<LeaveBalanceModel> CreateLeaveBalance([Required]CreateLeaveBalanceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveBalanceModel = _leaveBalanceRepository.GetLeaveBalanceByType(request.leavetype);
                if (leaveBalanceModel != null)
                {
                    //throw new Exception($"Leave balance already present for LeaveType {request.leavetype}");
                    return null;
                }

                return _leaveBalanceRepository.CreateLeaveBalance(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create leave balance {ex.Message}");
            }

        }

        [HttpPut("{leavetype}")]
        public ActionResult<LeaveBalanceModel> UpdateLeaveType([Required]string leavetype, [Required]UpdateLeaveBalanceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveBalanceModel = _leaveBalanceRepository.GetLeaveBalanceByType(leavetype);
                if (leaveBalanceModel == null)
                {
                    //throw new Exception($"Leave balance details not present for LeaveType {leavetype}");
                    return null;
                }

                return _leaveBalanceRepository.UpdateLeaveBalance(leavetype, request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update leave balance details {ex.Message}");
            }

        }

        [HttpDelete("{leavetype}")]
        public ActionResult<bool> DeleteLeaveType([Required] string leavetype)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveBalanceModel = _leaveBalanceRepository.GetLeaveBalanceByType(leavetype);
                if (leaveBalanceModel == null)
                {
                    //throw new Exception($"Leave balance details not present for LeaveType {leaveType}");
                    return null;
                }

                return _leaveBalanceRepository.DeleteLeaveBalance(leavetype);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete leave type details {ex.Message}");
            }

        }
    }
}