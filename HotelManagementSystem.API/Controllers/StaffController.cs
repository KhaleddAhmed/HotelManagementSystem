using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{
    public class StaffController : BaseApiController
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("GetAllUserReservations")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _staffService.GetAllUserReservationsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("ApproveOnReservation")]
        public async Task<ActionResult> Approve(int id)
        {
            var result = await _staffService.ApproveOnReservation(id);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("RejectOnReservation")]
        public async Task<ActionResult> Reject(int id)
        {
            var result = await _staffService.RejectOnReservationAsync(id);
            return Ok(result);
        }
    }
}
