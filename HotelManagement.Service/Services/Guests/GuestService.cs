using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Checkout;
using HotelManagement.Core.Dtos.Service;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Entities.Identity;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Service.Services.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GuestService(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GenericResponse<GetCheckoutDto>> CheckOutAsync(
            int reservationId,
            string userId
        )
        {
            var genericResponse = new GenericResponse<GetCheckoutDto>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(g => g.AppUserId == userId)
                .Result.Include(g => g.AppUser)
                .Include(g => g.UserServices)
                .ThenInclude(Us => Us.Service)
                .FirstOrDefaultAsync();
            if (guest is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "You are Not Guest To Checkout";

                return genericResponse;
            }
            var reservation = await _unitOfWork
                .Repository<Reservation, int>()
                .Get(r =>
                    r.Id == reservationId
                    && r.GuestID == guest.Id
                    && r.ReservationStatus == ReservationStatus.Approved
                )
                .Result.FirstOrDefaultAsync();

            if (reservation is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "You don't have reservations to checkout";

                return genericResponse;
            }

            var room = await _unitOfWork.Repository<Room, int>().GetAsync(reservation.RoomId);
            if (room is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Ivalid room to checkout";

                return genericResponse;
            }

            room.IsAvaliable = true;

            var result = await _userManager.RemoveFromRoleAsync(guest.AppUser, "Guest");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(guest.AppUser, "User");
                _unitOfWork.Repository<Room, int>().Update(room);

                var resultOfUpdate = await _unitOfWork.CompleteAsync();
                if (resultOfUpdate > 0)
                {
                    genericResponse.StatusCode = StatusCodes.Status200OK;
                    genericResponse.Message = "Success to Checkout";

                    var fees = 0.0m;
                    if (reservation.To.CompareTo(DateOnly.FromDateTime(DateTime.Now)) > 0)
                        fees = reservation.TotalPrice * 0.12m;

                    genericResponse.Data = new GetCheckoutDto
                    {
                        GuestName = guest.AppUser.UserName,
                        IsPaid = reservation.IsPaid,
                        AdditionalFees = (double)fees,
                        CheckoutDate = DateTime.Now,
                        ServicesFees = guest.UserServices.Sum(Us => Us.Service.ServiceFees),
                        TotalFees =
                            genericResponse.Data.ServicesFees + genericResponse.Data.AdditionalFees,
                    };

                    return genericResponse;
                }
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Checkout";
            return genericResponse;
        }

        public async Task<
            GenericResponse<List<GetAllAvailableServiceDto>>
        > GetAllAvailableServicesAsync()
        {
            var genericResponse = new GenericResponse<List<GetAllAvailableServiceDto>>();
            var services = await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .GetAllAsync();

            if (!services.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No services to Show";

                return genericResponse;
            }

            var mappedServices = _mapper.Map<List<GetAllAvailableServiceDto>>(services);

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retreive All services";
            genericResponse.Data = mappedServices;

            return genericResponse;
        }

        public async Task<
            GenericResponse<List<GetAllRequestedServicesForUserDTO>>
        > GetAllServicesAsync(string userId)
        {
            var genericResponse = new GenericResponse<List<GetAllRequestedServicesForUserDTO>>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(G => G.AppUserId == userId)
                .Result.Include(G => G.UserServices)
                .ThenInclude(US => US.Service)
                .FirstOrDefaultAsync();

            if (guest is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Guest To retrieve its Services";
                return genericResponse;
            }

            if (!guest.UserServices.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No services for This Guest to show";
                return genericResponse;
            }

            var mappedUserServices = _mapper.Map<List<GetAllRequestedServicesForUserDTO>>(
                guest.UserServices
            );

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retrieve all Guest Requested Services";
            genericResponse.Data = mappedUserServices;

            return genericResponse;
        }

        public async Task<GenericResponse<RequestedServiceDto>> GetRequestedServiceDetailsDto(
            string userId,
            int serviceId
        )
        {
            var genericResponse = new GenericResponse<RequestedServiceDto>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(G => G.AppUserId == userId)
                .Result.Include(G => G.AppUser)
                .Include(G => G.UserServices)
                .ThenInclude(Us => Us.Service)
                .FirstOrDefaultAsync();

            if (guest is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Guest to get its services";
                return genericResponse;
            }

            if (!guest.UserServices.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No services for This Guest";

                return genericResponse;
            }

            var service = guest.UserServices.FirstOrDefault(US => US.ServiceId == serviceId);

            if (service is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid service Id for this Guest";
                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retrieve Guest Service";
            genericResponse.Data = new RequestedServiceDto
            {
                GuestName = guest.AppUser.UserName,
                IsApproved = service.IsApproved,
                RequestedAt = service.RequestedAt,
                ServiceType = service.Service.ServiceType,
                ServiceFees = service.Service.ServiceFees,
            };

            return genericResponse;
        }

        public async Task<GenericResponse<bool>> RequestService(string userId, int serviceId)
        {
            var genericResponse = new GenericResponse<bool>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(G => G.AppUserId == userId)
                .Result.Include(G => G.UserServices)
                .FirstOrDefaultAsync();

            if (guest is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Guest to request service";

                return genericResponse;
            }
            var userServiceExist = guest.UserServices.FirstOrDefault(US =>
                US.ServiceId == serviceId && US.IsApproved == false
            );

            if (userServiceExist is not null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message =
                    "This service is requested by this user before and still pending";

                return genericResponse;
            }

            var userService = new UserService()
            {
                GuestId = guest.Id,
                ServiceId = serviceId,
                IsApproved = false,
            };

            await _unitOfWork.Repository<UserService, int>().AddAsync(userService);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to Request Service";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Request Service";
            genericResponse.Data = false;

            return genericResponse;
        }
    }
}
