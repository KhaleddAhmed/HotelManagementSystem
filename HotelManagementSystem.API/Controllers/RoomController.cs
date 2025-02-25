using HotelManagement.Core.Dtos.Room;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{
    public class RoomController : BaseApiController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            var result = await _roomService.CreateRoomAsync(roomDto);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> DeleteRoom(int roomId)
        {
            var result = await _roomService.DeleteRoomHardAsync(roomId);
            return Ok(result);
        }
    }
}
