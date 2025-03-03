using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core.Dtos.Reservation;
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
            CreateMap<UpdateRoomDto, Room>().ReverseMap();
            CreateMap<Room, AllRoomsDto>().ReverseMap();

            CreateMap<Reservation, GetReservationDto>()
                .ForMember(d => d.GuestName, o => o.MapFrom(s => s.Guest.AppUser.UserName))
                .ForMember(d => d.TotalNumberOfDays, o => o.MapFrom(s => s.To.Day - s.From.Day));
        }
    }
}
