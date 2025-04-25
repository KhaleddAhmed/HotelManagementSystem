using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Service;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Service.Services.ServiceImplementation
{
    public class FacilityService : IFacilitiesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FacilityService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<bool>> ApproveOnServicesAsync(int serviceId, int guestId)
        {
            var genericResponse = new GenericResponse<bool>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(G => G.Id == guestId)
                .Result.Include(G => G.UserServices)
                .FirstOrDefaultAsync();

            if (guest == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Non existed Guest";

                return genericResponse;
            }

            if (guest.UserServices.Any())
            {
                var userService = guest.UserServices.FirstOrDefault(US =>
                    US.ServiceId == serviceId
                );
                if (userService == null)
                {
                    genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                    genericResponse.Message = "User has No Requested service By This Id to approve";
                    return genericResponse;
                }

                userService.IsApproved = true;
                _unitOfWork.Repository<UserService, int>().Update(userService);
                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                    genericResponse.Message = "Sucess to Approve User service";
                    genericResponse.Data = true;

                    return genericResponse;
                }

                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Failed to Approve user Service";
                genericResponse.Data = false;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Approve user Service";
            genericResponse.Data = false;

            return genericResponse;
        }

        public async Task<GenericResponse<bool>> CreateServiceAsync(
            CreateServiecDto createServiecDto
        )
        {
            var genericResponse = new GenericResponse<bool>();
            if (createServiecDto is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Data";

                return genericResponse;
            }

            if (
                createServiecDto.ServiceType == ServiceType.HouseKeeping
                || createServiecDto.ServiceType == ServiceType.RequestMealOnRoom
            )
                createServiecDto.ServiceFees = 0;

            var mappedService = _mapper.Map<Core.Entities.Hotel.Service>(createServiecDto);

            await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .AddAsync(mappedService);

            var resultOfCreation = await _unitOfWork.CompleteAsync();
            if (resultOfCreation > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to Create Service";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Create Service";
            genericResponse.Data = false;

            return genericResponse;
        }

        public async Task<GenericResponse<bool>> DeleteServiceAsync(int serviceId)
        {
            var genericResponse = new GenericResponse<bool>();
            var service = await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .GetAsync(serviceId);
            if (service is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid service to Delete";
                genericResponse.Data = false;

                return genericResponse;
            }

            _unitOfWork.Repository<Core.Entities.Hotel.Service, int>().Delete(service);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to delete service";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to delete service";
            genericResponse.Data = false;

            return genericResponse;
        }

        public async Task<GenericResponse<List<GetAllServiceDto>>> GetAllServicesAsync()
        {
            var genericResponse = new GenericResponse<List<GetAllServiceDto>>();

            var Services = await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .GetAllAsync();
            if (!Services.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No services to show";
                return genericResponse;
            }

            var mappedServices = _mapper.Map<List<GetAllServiceDto>>(Services);

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retreive all Services";
            genericResponse.Data = mappedServices;
            return genericResponse;
        }

        public async Task<GenericResponse<List<UserServiceApprovementDto>>> GetAllUsersServices()
        {
            var genericResponse = new GenericResponse<List<UserServiceApprovementDto>>();
            var userServices = await _unitOfWork
                .Repository<UserService, int>()
                .Get()
                .Result.Include(Us => Us.Guest)
                .ThenInclude(G => G.AppUser)
                .Include(Us => Us.Service)
                .ToListAsync();

            if (!userServices.Any())
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "No users Requested Services to Show";

                return genericResponse;
            }

            var mappedUserServiceApprovement = _mapper.Map<List<UserServiceApprovementDto>>(
                userServices
            );

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retreive all users services";
            genericResponse.Data = mappedUserServiceApprovement;
            return genericResponse;
        }

        public async Task<GenericResponse<GetServicDetails>> GetServiceDetailsAsync(int serviceId)
        {
            var genericResponse = new GenericResponse<GetServicDetails>();
            var service = await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .GetAsync(serviceId);
            if (service is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid service to get details";
                return genericResponse;
            }

            var mappedService = _mapper.Map<GetServicDetails>(service);
            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Success to retreive service details";
            genericResponse.Data = mappedService;

            return genericResponse;
        }

        public async Task<GenericResponse<bool>> RejectOnServicesAsync(int serviceId, int guestId)
        {
            var genericResponse = new GenericResponse<bool>();

            var guest = await _unitOfWork
                .Repository<Guest, int>()
                .Get(G => G.Id == guestId)
                .Result.Include(G => G.UserServices)
                .FirstOrDefaultAsync();

            if (guest == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Non existed Guest";

                return genericResponse;
            }

            if (guest.UserServices.Any())
            {
                var userService = guest.UserServices.FirstOrDefault(US =>
                    US.ServiceId == serviceId
                );
                if (userService == null)
                {
                    genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                    genericResponse.Message = "User has No Requested service By This Id to Reject";
                    return genericResponse;
                }

                userService.IsApproved = false;
                _unitOfWork.Repository<UserService, int>().Update(userService);
                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                    genericResponse.Message = "Sucess to Approve User service";
                    genericResponse.Data = true;

                    return genericResponse;
                }

                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Failed to Approve user Service";
                genericResponse.Data = false;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Approve user Service";
            genericResponse.Data = false;

            return genericResponse;
        }

        public async Task<GenericResponse<bool>> UpdateServiceAsync(
            UpdateServiceDto updateServiceDto
        )
        {
            var genericResponse = new GenericResponse<bool>();
            if (updateServiceDto is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid data to update service";

                return genericResponse;
            }

            var serviceToUpdate = await _unitOfWork
                .Repository<Core.Entities.Hotel.Service, int>()
                .GetAsync(updateServiceDto.Id);
            ;

            if (serviceToUpdate is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid service to update";
                genericResponse.Data = false;
                return genericResponse;
            }

            _mapper.Map(updateServiceDto, serviceToUpdate);
            serviceToUpdate.ModifiedAt = DateTime.Now;
            _unitOfWork.Repository<Core.Entities.Hotel.Service, int>().Update(serviceToUpdate);
            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Success to update service";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Update Service";
            genericResponse.Data = false;
            return genericResponse;
        }
    }
}
