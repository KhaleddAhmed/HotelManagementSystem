using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core.Dtos.Room;
using HotelManagement.Core.Entities.Hotel;

namespace HotelManagement.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateRoomDto, Room>();
            CreateMap<Room, GetRoomDto>().ReverseMap();
        }
    }
}
