using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Dtos.Room
{
    public class CreateRoomDto
    {
        public string RoomType { get; set; }

        public decimal Price { get; set; }

        public int NumberOfBeds { get; set; }

        public bool IsSea { get; set; }
    }
}
