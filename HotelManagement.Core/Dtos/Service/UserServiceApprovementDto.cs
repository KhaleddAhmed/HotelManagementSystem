using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Service
{
    public class UserServiceApprovementDto
    {
        public int ServiceId { get; set; }
        public string GuestName { get; set; }

        public int GuestId { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime RequestedAt { get; set; }

        public bool IsApproved { get; set; }
    }
}
