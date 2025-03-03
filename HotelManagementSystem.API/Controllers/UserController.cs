using System.Security.Claims;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IReservationService _reservationService;

        public UserController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("Reserve")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _reservationService.CreateReservationAsync(
                userId,
                createReservationDto
            );

            return Ok(result);
        }
    }
}
