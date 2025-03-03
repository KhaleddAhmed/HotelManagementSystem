﻿using System;
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
            var guest = new Guest { AppUserId = userId, LoyaltyPoints = 0 };

            await _unitOfWork.Repository<Guest, int>().AddAsync(guest);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                await _userManager.AddToRoleAsync(user, "Guest");
                var Reservation = new Reservation
                {
                    GuestID = guest.Id,
                    RoomId = room.Id,
                    From = createReservationDto.From,
                    To = createReservationDto.To,
                    TotalPrice =
                        room.Price * (createReservationDto.To.Day - createReservationDto.From.Day),
                    ReservationCreatedAt = DateTime.Now,
                    PaymentMethod = createReservationDto.PaymentMethod,
                };

                await _unitOfWork.Repository<Reservation, int>().AddAsync(Reservation);

                var resultReservation = await _unitOfWork.CompleteAsync();

                if (resultReservation > 0)
                {
                    var mappedReservation = _mapper.Map<GetReservationDto>(Reservation);

                    genericResponse.StatusCode = StatusCodes.Status200OK;
                    genericResponse.Message = "Reservation Created Successfully";
                    genericResponse.Data = mappedReservation;

                    return genericResponse;
                }
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Create Reservation";

            return genericResponse;
        }

        public Task<GenericResponse<bool>> DeleteReservationAsync(int reservationId)
        {
            throw new NotImplementedException();
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
    }
}
