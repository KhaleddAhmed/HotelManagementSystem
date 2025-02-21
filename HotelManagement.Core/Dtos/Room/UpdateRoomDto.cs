using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Room
{
    public class UpdateRoomDto
    {
        public int Id { get; set; }
        public string RoomType { get; set; }

        public bool IsAvaliable { get; set; }

        public decimal Price { get; set; }

        public int NumberOfBeds { get; set; }

        public string RoomView { get; set; }

        public string ExtraPrivilege { get; set; }
    }
}
