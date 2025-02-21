using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Room
{
    public class AllRoomsDto
    {
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBeds { get; set; }
        public double Rate { get; set; }
        public int NumberOfReviewers { get; set; }
    }
}
