using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IStaffService
    {
        Task<GenericResponse<List<GetAllUserReservationsDto>>> GetAllUserReservationsAsync();
        Task<GenericResponse<bool>> ApproveOnReservation(int reservationId);
        Task<GenericResponse<bool>> RejectOnReservationAsync(int reservationId);
    }
}
