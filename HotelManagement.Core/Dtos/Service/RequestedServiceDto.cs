using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Service
{
    public class RequestedServiceDto
    {
        public string GuestName { get; set; }
        public ServiceType ServiceType { get; set; }

        public DateTime RequestedAt { get; set; }
        public bool IsApproved { get; set; }

        public double ServiceFees { get; set; }
    }
}
