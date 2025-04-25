using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Checkout;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Entities.Identity;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Service.Services.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public GuestService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<GenericResponse<GetCheckoutDto>> CheckOutAsync(
            int reservationId,
            string userId
        )
        {
            var genericResponse = new GenericResponse<GetCheckoutDto>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(g => g.AppUserId == userId)
                .Result.Include(g => g.AppUser)
                .FirstOrDefaultAsync();
            if (guest is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "You are Not Guest To Checkout";

                return genericResponse;
            }
            var reservation = await _unitOfWork
                .Repository<Reservation, int>()
                .Get(r =>
                    r.Id == reservationId
                    && r.GuestID == guest.Id
                    && r.ReservationStatus == ReservationStatus.Approved
                )
                .Result.FirstOrDefaultAsync();

            if (reservation is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "You don't have reservations to checkout";

                return genericResponse;
            }

            var room = await _unitOfWork.Repository<Room, int>().GetAsync(reservation.RoomId);
            if (room is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Ivalid room to checkout";

                return genericResponse;
            }

            room.IsAvaliable = true;

            _unitOfWork.Repository<Room, int>().Update(room);

            var resultOfUpdate = await _unitOfWork.CompleteAsync();
            if (resultOfUpdate > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to Checkout";

                var fees = 0.0m;
                if (reservation.To.CompareTo(DateOnly.FromDateTime(DateTime.Now)) > 0)
                    fees = reservation.TotalPrice * 0.12m;

                genericResponse.Data = new GetCheckoutDto
                {
                    GuestName = guest.AppUser.UserName,
                    IsPaid = reservation.IsPaid,
                    AdditionalFees = (double)fees,
                    CheckoutDate = DateTime.Now,
                    ServicesFees = 0,
                };

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Checkout";
            return genericResponse;
        }
    }
}
