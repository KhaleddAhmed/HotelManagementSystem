using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Service.Services.staff
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<bool>> ApproveOnReservation(int reservationId)
        {
            var genericResponse = new GenericResponse<bool>();
            var reservation = await _unitOfWork
                .Repository<Reservation, int>()
                .Get(r => r.Id == reservationId)
                .Result.FirstOrDefaultAsync();
            if (reservation is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid reservation to approve";

                return genericResponse;
            }

            reservation.ReservationStatus = ReservationStatus.Approved;

            _unitOfWork.Repository<Reservation, int>().Update(reservation);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to approve Reservation";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to approve Reservation";
            genericResponse.Data = false;

            return genericResponse;
        }

        public async Task<
            GenericResponse<List<GetAllUserReservationsDto>>
        > GetAllUserReservationsAsync()
        {
            var genericResponse = new GenericResponse<List<GetAllUserReservationsDto>>();
            var reservations = await _unitOfWork
                .Repository<Reservation, int>()
                .GetAllAsyncAsQueryable()
                .Result.Include(R => R.Guest)
                .ThenInclude(g => g.AppUser)
                .ToListAsync();

            if (!reservations.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No reservations to show";

                return genericResponse;
            }

            var mappedReservation = _mapper.Map<List<GetAllUserReservationsDto>>(reservations);

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to show all reservations";
            genericResponse.Data = mappedReservation;
            return genericResponse;
        }

        public async Task<GenericResponse<bool>> RejectOnReservationAsync(int reservationId)
        {
            var genericResponse = new GenericResponse<bool>();
            var reservation = await _unitOfWork
                .Repository<Reservation, int>()
                .Get(r => r.Id == reservationId)
                .Result.FirstOrDefaultAsync();
            if (reservation is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid reservation to Reject";

                return genericResponse;
            }

            reservation.ReservationStatus = ReservationStatus.Rejected;

            _unitOfWork.Repository<Reservation, int>().Update(reservation);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to Reject Reservation";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Reject Reservation";
            genericResponse.Data = false;

            return genericResponse;
        }
    }
}
