using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IReservationService
    {
        Task<GenericResponse<GetReservationDto>> CreateReservationAsync(
            string userId,
            CreateReservationDto createReservationDto
        );

        Task<GenericResponse<GetReservationDto>> UpdateReservationAsync(
            UpdateReservationDto updateReservationDto
        );

        Task<GenericResponse<GenericResponse<GetReservationDto>>> GetReservationDetailsAsync(
            int reservationId
        );

        Task<GenericResponse<bool>> DeleteReservationAsync(int reservationId);

        Task<GenericResponse<GetAllReservationsDto>> GetAllReservationsAsync(string? userId);
    }
}
