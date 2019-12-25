using LeaveApplicationUsingMongoDB.Models;
using LeaveApplicationUsingMongoDB.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveApplicationUsingMongoDB.Controllers
{
    [Route("api/leave/type")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        LeaveTypeRepository _leaveTypeRepository;
        public LeaveTypeController()
        {
            _leaveTypeRepository = new LeaveTypeRepository();
        }

        [HttpGet]
        public ActionResult<List<LeaveTypeModel>> Get()
        {
            try
            {
                var leaveTypeModel = _leaveTypeRepository.GetLeaveTypes();
                if (leaveTypeModel == null)
                {
                    throw new Exception($"Leave type details not found");
                }

                return leaveTypeModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leave type details {ex.Message}");
            }

        }

        [HttpGet("{leavetype}")]
        public ActionResult<LeaveTypeModel> Get([Required]string leavetype)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveTypeModel = _leaveTypeRepository.GetLeaveTypes(leavetype);
                if (leaveTypeModel == null)
                {
                    //throw new Exception($"Leave type details not found for leave type {leavetype}");
                    return null;
                }

                return leaveTypeModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leave type details {ex.Message}");
            }

        }

        [HttpPost]
        public ActionResult<LeaveTypeModel> CreateLeaveType([Required]CreateLeaveTypeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveTypeModel = _leaveTypeRepository.GetLeaveTypes(request.leavetype);
                if (leaveTypeModel != null)
                {
                    //throw new Exception($"Leave type already present for LeaveType {request.leavetype}");
                    return null;
                }

                return _leaveTypeRepository.CreateLeaveType(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create leave type {ex.Message}");
            }

        }

        [HttpPut("{leavetype}")]
        public ActionResult<LeaveTypeModel> UpdateLeaveType([Required]string leavetype, [Required]UpdateLeaveTypeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Model state invalid");
                }

                var leaveTypeModel = _leaveTypeRepository.GetLeaveTypes(leavetype);
                if (leaveTypeModel == null)
                {
                    //throw new Exception($"Leave type details not present for LeaveType {leavetype}");
                    return null;
                }

                return _leaveTypeRepository.UpdateLeaveType(leavetype, request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update leave type details {ex.Message}");
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

                var leaveTypeModel = _leaveTypeRepository.GetLeaveTypes(leavetype);
                if (leaveTypeModel == null)
                {
                    //throw new Exception($"Leave type details not present for LeaveType {leaveType}");
                    return null;
                }

                return _leaveTypeRepository.DeleteLeaveType(leavetype);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete leave type details {ex.Message}");
            }

        }
    }
}