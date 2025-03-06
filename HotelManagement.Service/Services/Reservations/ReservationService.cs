using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Entities.Identity;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Service.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        //public async Task<GenericResponse<GetReservationDto>> CreateReservationAsync(
        //    string userId,
        //    CreateReservationDto createReservationDto
        //)
        //{
        //    var genericResponse = new GenericResponse<GetReservationDto>();
        //    if (createReservationDto == null)
        //    {
        //        genericResponse.StatusCode = StatusCodes.Status400BadRequest;
        //        genericResponse.Message = "Enter All required Data";

        //        return genericResponse;
        //    }

        //    var room = await _unitOfWork
        //        .Repository<Room, int>()
        //        .GetAsync(createReservationDto.RoomId);
        //    if (room == null)
        //    {
        //        genericResponse.StatusCode = StatusCodes.Status400BadRequest;
        //        genericResponse.Message = "Invalid Room Id";

        //        return genericResponse;
        //    }

        //    if (room.IsAvaliable == false)
        //    {
        //        genericResponse.StatusCode = StatusCodes.Status400BadRequest;
        //        genericResponse.Message = "Room Is Not Avaliable";

        //        return genericResponse;
        //    }

        //    if (createReservationDto.From.CompareTo(createReservationDto.To) > 0)
        //    {
        //        genericResponse.StatusCode = StatusCodes.Status400BadRequest;
        //        genericResponse.Message = "Invalid Reservation Duration";

        //        return genericResponse;
        //    }

        //    var user = await _unitOfWork.Repository<AppUser, string>().GetAsync(userId);
        //    if (user == null)
        //    {
        //        genericResponse.StatusCode = StatusCodes.Status400BadRequest;
        //        genericResponse.Message = "Invalid User Id";

        //        return genericResponse;
        //    }

        //    var oldGuest=await _unitOfWork.Repository<Guest, int>().Get(g=>g.AppUserId == userId).Result.FirstOrDefaultAsync();
        //    if (oldGuest is not null)
        //    {
        //        var Reservation = new Reservation
        //        {
        //            GuestID = oldGuest.Id,
        //            RoomId = room.Id,
        //            From = createReservationDto.From,
        //            To = createReservationDto.To,
        //            TotalPrice =
        //                room.Price * (createReservationDto.To.Day - createReservationDto.From.Day),
        //            ReservationCreatedAt = DateTime.Now,
        //            PaymentMethod = createReservationDto.PaymentMethod,
        //        };

        //        await _unitOfWork.Repository<Reservation, int>().AddAsync(Reservation);

        //        room.IsAvaliable = false;

        //        var resultReservation = await _unitOfWork.CompleteAsync();

        //        if (resultReservation > 0)
        //        {
        //            var mappedReservation = _mapper.Map<GetReservationDto>(Reservation);

        //            if (createReservationDto.From.Day > createReservationDto.To.Day)
        //            {
        //                mappedReservation.TotalNumberOfDays =
        //                    (createReservationDto.To.Day + 31) - createReservationDto.From.Day;
        //            }

        //            genericResponse.StatusCode = StatusCodes.Status200OK;
        //            genericResponse.Message = "Reservation Created Successfully";
        //            genericResponse.Data = mappedReservation;

        //            return genericResponse;
        //        }
        //    }
        //    var guest = new Guest { AppUserId = userId, LoyaltyPoints = 0 };

        //    await _unitOfWork.Repository<Guest, int>().AddAsync(guest);
        //    var result = await _unitOfWork.CompleteAsync();

        //    if (result > 0)
        //    {
        //        await _userManager.AddToRoleAsync(user, "Guest");
        //        var Reservation = new Reservation
        //        {
        //            GuestID = guest.Id,
        //            RoomId = room.Id,
        //            From = createReservationDto.From,
        //            To = createReservationDto.To,
        //            TotalPrice =
        //                room.Price * (createReservationDto.To.Day - createReservationDto.From.Day),
        //            ReservationCreatedAt = DateTime.Now,
        //            PaymentMethod = createReservationDto.PaymentMethod,
        //        };

        //        await _unitOfWork.Repository<Reservation, int>().AddAsync(Reservation);

        //        room.IsAvaliable = false;

        //        var resultReservation = await _unitOfWork.CompleteAsync();

        //        if (resultReservation > 0)
        //        {
        //            var mappedReservation = _mapper.Map<GetReservationDto>(Reservation);

        //            genericResponse.StatusCode = StatusCodes.Status200OK;
        //            genericResponse.Message = "Reservation Created Successfully";
        //            genericResponse.Data = mappedReservation;

        //            return genericResponse;
        //        }
        //    }

        //    genericResponse.StatusCode = StatusCodes.Status200OK;
        //    genericResponse.Message = "Failed to Create Reservation";

        //    return genericResponse;
        //}
        public async Task<GenericResponse<GetReservationDto>> CreateReservationAsync(
            string userId,
            CreateReservationDto createReservationDto
        )
        {
            var genericResponse = new GenericResponse<GetReservationDto>();

            if (createReservationDto == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Enter All required Data";
                return genericResponse;
            }

            var room = await _unitOfWork
                .Repository<Room, int>()
                .GetAsync(createReservationDto.RoomId);
            if (room == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Room Id";
                return genericResponse;
            }

            if (!room.IsAvaliable)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Room Is Not Avaliable";
                return genericResponse;
            }

            if (createReservationDto.From.CompareTo(createReservationDto.To) > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Reservation Duration";
                return genericResponse;
            }

            var user = await _unitOfWork.Repository<AppUser, string>().GetAsync(userId);
            if (user == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid User Id";
                return genericResponse;
            }

            var oldGuest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(g => g.AppUserId == userId)
                .Result.FirstOrDefaultAsync();
            if (oldGuest != null)
            {
                var reservation = new Reservation
                {
                    GuestID = oldGuest.Id,
                    RoomId = room.Id,
                    From = createReservationDto.From,
                    To = createReservationDto.To,
                    TotalPrice = CalculateTotalPrice(
                        createReservationDto.From,
                        createReservationDto.To,
                        room.Price
                    ),
                    ReservationCreatedAt = DateTime.Now,
                    PaymentMethod = createReservationDto.PaymentMethod,
                };

                await _unitOfWork.Repository<Reservation, int>().AddAsync(reservation);

                room.IsAvaliable = false;

                var resultReservation = await _unitOfWork.CompleteAsync();

                if (resultReservation > 0)
                {
                    var mappedReservation = _mapper.Map<GetReservationDto>(reservation);

                    genericResponse.StatusCode = StatusCodes.Status200OK;
                    genericResponse.Message = "Reservation Created Successfully";
                    genericResponse.Data = mappedReservation;

                    return genericResponse;
                }
            }

            var guest = new Guest { AppUserId = userId, LoyaltyPoints = 0 };
            await _unitOfWork.Repository<Guest, int>().AddAsync(guest);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                await _userManager.AddToRoleAsync(user, "Guest");
                var reservation = new Reservation
                {
                    GuestID = guest.Id,
                    RoomId = room.Id,
                    From = createReservationDto.From,
                    To = createReservationDto.To,
                    TotalPrice = CalculateTotalPrice(
                        createReservationDto.From,
                        createReservationDto.To,
                        room.Price
                    ),
                    ReservationCreatedAt = DateTime.Now,
                    PaymentMethod = createReservationDto.PaymentMethod,
                };

                await _unitOfWork.Repository<Reservation, int>().AddAsync(reservation);

                room.IsAvaliable = false;

                var resultReservation = await _unitOfWork.CompleteAsync();

                if (resultReservation > 0)
                {
                    var mappedReservation = _mapper.Map<GetReservationDto>(reservation);

                    genericResponse.StatusCode = StatusCodes.Status200OK;
                    genericResponse.Message = "Reservation Created Successfully";
                    genericResponse.Data = mappedReservation;

                    return genericResponse;
                }
            }

            genericResponse.StatusCode = StatusCodes.Status400BadRequest;
            genericResponse.Message = "Failed to Create Reservation";
            return genericResponse;
        }

        public async Task<GenericResponse<bool>> DeleteReservationAsync(int reservationId)
        {
            var genericResponse = new GenericResponse<bool>();
            var reservation = await _unitOfWork
                .Repository<Reservation, int>()
                .GetAsync(reservationId);
            if (reservation == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Reservation to delete";

                return genericResponse;
            }

            reservation.IsDeleted = true;

            _unitOfWork.Repository<Reservation, int>().Update(reservation);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Successfully delete reservation";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to delete reservation";

            return genericResponse;
        }

        public Task<GenericResponse<GetAllReservationsDto>> GetAllReservationsAsync(string? userId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<GenericResponse<GetReservationDto>>> GetReservationDetailsAsync(
            int reservationId
        )
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<GetReservationDto>> UpdateReservationAsync(
            UpdateReservationDto updateReservationDto
        )
        {
            throw new NotImplementedException();
        }

        private decimal CalculateTotalPrice(DateOnly fromDate, DateOnly toDate, decimal roomPrice)
        {
            // Calculate the total number of days between the two dates
            var totalDays = (
                toDate.ToDateTime(new TimeOnly()) - fromDate.ToDateTime(new TimeOnly())
            ).Days;

            // If the total days are negative, handle the case appropriately
            if (totalDays < 0)
            {
                throw new InvalidOperationException("Reservation duration cannot be negative.");
            }

            // Calculate and return the total price
            return roomPrice * totalDays;
        }
    }
}
