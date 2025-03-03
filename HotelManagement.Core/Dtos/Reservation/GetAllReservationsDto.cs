using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Reservation
{
    public class GetAllReservationsDto
    {
        public string GuestName { get; set; }

        public DateOnly From { get; set; }
        public DateOnly To { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
