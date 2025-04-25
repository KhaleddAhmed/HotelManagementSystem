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
    }
}
