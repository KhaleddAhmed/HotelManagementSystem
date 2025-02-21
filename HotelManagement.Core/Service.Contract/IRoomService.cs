using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Room;

namespace HotelManagement.Core.Service.Contract
{
    public interface IRoomService
    {
        Task<AllRoomsDto> GetAllRoomsAsync(int? pageSize, int? PageIndex);

        Task<GetRoomDto> GetRoomByIdAsync(int id);

        Task<GetRoomDto> CreateRoomAsync(CreateRoomDto room);

        Task<GetRoomDto> UpdateRoomAsync(UpdateRoomDto room);

        Task DeleteRoomAsync(int id);
    }
}
