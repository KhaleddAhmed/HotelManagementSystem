using System.Security.Claims;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{
    public class GuestController : BaseApiController
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPut("CheckOut")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> CheckOut(int reservationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _guestService.CheckOutAsync(reservationId, userId);
            return Ok(result);
        }

        [HttpGet("GetAllAvailableServices")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _guestService.GetAllAvailableServicesAsync();
            return Ok(result);
        }

        [HttpGet("GetAllMyRequestedServices")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> GetAllRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _guestService.GetAllServicesAsync(userId);
            return Ok(result);
        }

        [HttpGet("GetRequestedServiceDetails")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> GetRequestDetails(int serviceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _guestService.GetRequestedServiceDetailsDto(userId, serviceId);
            return Ok(result);
        }

        [HttpPost("RequestService")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> RequestService(int serviceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _guestService.RequestService(userId, serviceId);
            return Ok(result);
        }
    }
}
