using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Hotel
{
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
    }

    public enum ReservationStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }

    public class Reservation : BaseEntity
    {
        public int GuestID { get; set; }
        public Guest Guest { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public decimal TotalPrice { get; set; }

        public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Pending;

        public string? ApprovedBy { get; set; }

        public DateTime ReservationCreatedAt { get; set; } = DateTime.Now;
        public DateTime? ReservationModifiedAt { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
