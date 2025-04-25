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
        Task<GenericResponse<GetReservationDtoWithToken>> CreateReservationAsync(
            string userId,
            CreateReservationDto createReservationDto
        );

        Task<GenericResponse<GetReservationDto>> UpdateReservationAsync(
            UpdateReservationDto updateReservationDto
        );

        Task<GenericResponse<GetReservationDto>> GetReservationDetailsAsync(
            int reservationId,
            string userId
        );

        Task<GenericResponse<bool>> DeleteReservationAsync(int reservationId);

        Task<GenericResponse<List<GetAllReservationsDto>>> GetAllReservationsAsync(string? userId);
    }
}
