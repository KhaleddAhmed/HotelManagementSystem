using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core.Dtos.Reservation;
using HotelManagement.Core.Dtos.Room;
using HotelManagement.Core.Dtos.Service;
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

            CreateMap<Reservation, GetReservationDtoWithToken>()
                .ForMember(d => d.GuestName, o => o.MapFrom(s => s.Guest.AppUser.UserName))
                // Calculate TotalNumberOfDays by considering the full duration between the From and To dates
                .ForMember(
                    d => d.TotalNumberOfDays,
                    o =>
                        o.MapFrom(s =>
                            Math.Max(
                                0,
                                (
                                    s.To.ToDateTime(new TimeOnly())
                                    - s.From.ToDateTime(new TimeOnly())
                                ).Days
                            )
                        )
                );

            CreateMap<Reservation, GetReservationDto>()
                .ForMember(d => d.GuestName, o => o.MapFrom(s => s.Guest.AppUser.UserName))
                // Calculate TotalNumberOfDays by considering the full duration between the From and To dates
                .ForMember(
                    d => d.TotalNumberOfDays,
                    o =>
                        o.MapFrom(s =>
                            Math.Max(
                                0,
                                (
                                    s.To.ToDateTime(new TimeOnly())
                                    - s.From.ToDateTime(new TimeOnly())
                                ).Days
                            )
                        )
                );

            CreateMap<Reservation, GetAllReservationsDto>()
                .ForMember(d => d.ReservationId, o => o.MapFrom(S => S.Id));

            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();

            CreateMap<CreateServiecDto, Core.Entities.Hotel.Service>();
            CreateMap<Core.Entities.Hotel.Service, GetAllServiceDto>();
            CreateMap<UpdateServiceDto, Core.Entities.Hotel.Service>();

            CreateMap<Core.Entities.Hotel.Service, GetServicDetails>();

            CreateMap<UserService, UserServiceApprovementDto>()
                .ForMember(d => d.GuestName, o => o.MapFrom(S => S.Guest.AppUser.UserName))
                .ForMember(d => d.ServiceType, o => o.MapFrom(S => S.Service.ServiceType));
        }
    }
}
