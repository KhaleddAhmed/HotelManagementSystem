using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Room
{
    public class GetAllRoomsDtoWithCount
    {
        public int Count { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public List<AllRoomsDto> AllRoomsDtos { get; set; }
    }
}
