using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Reservation
{
    public class GetAllUserReservationsDto
    {
        public int Id { get; set; }
        public string GuestName { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
