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

        [HttpDelete("CancelReservation")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> CancelReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservationAsync(reservationId);
            return Ok(result);
        }

        [HttpGet("GetAllMyReservations")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> GetAllReservtions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reservationService.GetAllReservationsAsync(userId);
            return Ok(result);
        }

        [HttpGet("GetReservationDetails")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> GetReservationDetails(int reservationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _reservationService.GetReservationDetailsAsync(
                reservationId,
                userId
            );
            return Ok(result);
        }

        [HttpPut("UpdateReservation")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var result = await _reservationService.UpdateReservationAsync(updateReservationDto);
            return Ok(result);
        }
    }
}
