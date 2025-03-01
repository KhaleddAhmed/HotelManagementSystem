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

        public async Task<GenericResponse<bool>> DeleteRoomHardAsync(int id)
        {
            GenericResponse<bool> genericResponse = new GenericResponse<bool>();
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(id);
            if (room == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Room Id";
                return genericResponse;
            }

            _unitOfWork.Repository<Room, int>().Delete(room);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Room is deleted Successfully";
                genericResponse.Data = true;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status500InternalServerError;
            genericResponse.Message = "Failed to delete room";
            genericResponse.Data = false;

            return genericResponse;
        }

        public Task<GenericResponse<bool>> DeleteRoomSoftAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<GetAllRoomsDtoWithCount>> GetAllRoomsAsync(
            int? pageSize,
            int? PageIndex
        )
        {
            var GenericResponse = new GenericResponse<GetAllRoomsDtoWithCount>();
            GetAllRoomsDtoWithCount getAllRoomsDtoWithCount = new GetAllRoomsDtoWithCount();

            var rooms = await _unitOfWork.Repository<Room, int>().GetAllAsync();

            if (rooms is not null)
            {
                var paginatedRooms = rooms.Skip(PageIndex.Value - 1).Take(pageSize.Value).ToList();
                var mappedRooms = _mapper.Map<List<AllRoomsDto>>(paginatedRooms);
                getAllRoomsDtoWithCount.Count = rooms.Count();
                getAllRoomsDtoWithCount.AllRoomsDtos = mappedRooms;
                getAllRoomsDtoWithCount.PageSize = pageSize.Value;
                getAllRoomsDtoWithCount.PageIndex = PageIndex.Value;

                GenericResponse.StatusCode = StatusCodes.Status200OK;
                GenericResponse.Message = "All Rooms Retreived succesfully";
                GenericResponse.Data = getAllRoomsDtoWithCount;

                return GenericResponse;
            }

            GenericResponse.StatusCode = StatusCodes.Status200OK;
            GenericResponse.Message = "No rooms to show";

            return GenericResponse;
        }

        public async Task<GenericResponse<GetRoomDto>> GetRoomByIdAsync(int id)
        {
            var genericResponse = new GenericResponse<GetRoomDto>();
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(id);
            if (room is not null)
            {
                var mappedRoom = _mapper.Map<GetRoomDto>(room);
                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Room details retrieved sucessfully";
                genericResponse.Data = mappedRoom;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status400BadRequest;
            genericResponse.Message = "Invalid room Id";

            return genericResponse;
        }

        public async Task<GenericResponse<GetRoomDto>> UpdateRoomAsync(UpdateRoomDto Updateroom)
        {
            var genericResponse = new GenericResponse<GetRoomDto>();

            if (Updateroom is null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "There are Invalid Inputs";

                return genericResponse;
            }

            if (Updateroom.NumberOfBeds > 10)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "This is Invalid Number of Beds";
                genericResponse.Data = null;

                return genericResponse;
            }

            var listOfAllRoomTypes = GetAllRoomTypes();

            if (!listOfAllRoomTypes.Contains(Updateroom.RoomType))
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "This is not valid room type";
                genericResponse.Data = null;

                return genericResponse;
            }

            if (Updateroom.Price < 0)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Price";
                genericResponse.Data = null;

                return genericResponse;
            }
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(Updateroom.Id);
            if (room == null)
            {
                genericResponse.StatusCode = StatusCodes.Status400BadRequest;
                genericResponse.Message = "Invalid Room Id";

                return genericResponse;
            }

            _mapper.Map(Updateroom, room);

            room.ModifiedAt = DateTime.Now;

            _unitOfWork.Repository<Room, int>().Update(room);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                var mappedRoom = _mapper.Map<GetRoomDto>(room);

                genericResponse.StatusCode = StatusCodes.Status200OK;
                genericResponse.Message = "Room Updated Succesfully";
                genericResponse.Data = mappedRoom;

                return genericResponse;
            }

            genericResponse.StatusCode = StatusCodes.Status200OK;
            genericResponse.Message = "Failed to Update room";

            return genericResponse;
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
