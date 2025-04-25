using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Hotel
{
    public class UserService
    {
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;
    }
}
