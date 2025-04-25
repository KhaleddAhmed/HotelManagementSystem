using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Service
{
    public class GetAllRequestedServicesForUserDTO
    {
        public int ServiceId { get; set; }
        public ServiceType ServiceType { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
