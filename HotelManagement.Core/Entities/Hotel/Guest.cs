using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Identity;

namespace HotelManagement.Core.Entities.Hotel
{
    public class Guest : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public int LoyaltyPoints { get; set; }

        public virtual ICollection<UserService> UserServices { get; set; }
    }
}
