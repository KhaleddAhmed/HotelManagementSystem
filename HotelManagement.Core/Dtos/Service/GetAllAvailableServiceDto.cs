using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Service
{
    public class GetAllAvailableServiceDto
    {
        public int Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public double ServiceFees { get; set; }
    }
}
