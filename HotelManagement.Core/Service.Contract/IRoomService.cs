using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Room;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IRoomService
    {
        Task<GenericResponse<GetAllRoomsDtoWithCount>> GetAllRoomsAsync(
            int? pageSize,
            int? PageIndex
        );

        Task<GenericResponse<GetRoomDto>> GetRoomByIdAsync(int id);

        Task<GenericResponse<GetRoomDto>> CreateRoomAsync(CreateRoomDto room);

        Task<GenericResponse<GetRoomDto>> UpdateRoomAsync(UpdateRoomDto room);

        Task<GenericResponse<bool>> DeleteRoomSoftAsync(int id);

        Task<GenericResponse<bool>> DeleteRoomHardAsync(int id);
    }
}
