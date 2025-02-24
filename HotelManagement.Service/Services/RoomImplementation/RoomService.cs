using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Dtos.Room;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Repositories.Contract;
using HotelManagement.Core.Responses;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Service.Services.RoomImplementation
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<GetRoomDto>> CreateRoomAsync(CreateRoomDto room)
        {
            var genericResponse = new GenericResponse<GetRoomDto>();
            if (room == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "There are Invalid Inputs";
                genericResponse.Data = null;

                return genericResponse;
            }

            if (room.NumberOfBeds > 10)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "This is Invalid Number of Beds";
                genericResponse.Data = null;

                return genericResponse;
            }

            var listOfAllRoomTypes = GetAllRoomTypes();

            if (!listOfAllRoomTypes.Contains(room.RoomType))
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "This is not valid room type";
                genericResponse.Data = null;

                return genericResponse;
            }

            if (room.Price < 0)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Price";
                genericResponse.Data = null;

                return genericResponse;
            }

            var RoomToCreate = _mapper.Map<Room>(room);

            RoomToCreate.NumberOfReviewers = 0;
            RoomToCreate.Rate = 0.0;
            RoomToCreate.IsAvaliable = true;
            await _unitOfWork.Repository<Room, int>().AddAsync(RoomToCreate);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                var mappedRoom = _mapper.Map<GetRoomDto>(RoomToCreate);

                genericResponse.StatusCode = StatusCodes.Status201Created;
                genericResponse.Message = "Succedded to Create Room";
                genericResponse.Data = mappedRoom;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Create Room";
            genericResponse.Data = null;

            return genericResponse;
        }

        public Task<GenericResponse<bool>> DeleteRoomHardAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<bool>> DeleteRoomSoftAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<AllRoomsDto>> GetAllRoomsAsync(int? pageSize, int? PageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<GetRoomDto>> GetRoomByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<GetRoomDto>> UpdateRoomAsync(UpdateRoomDto room)
        {
            throw new NotImplementedException();
        }

        private List<string> GetAllRoomTypes()
        {
            var list = new List<string>()
            {
                "Single",
                "Double",
                "Triple",
                "Suite",
                "Royal Suite",
                "King",
            };

            return list;
        }
    }
}
