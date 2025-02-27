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

        [HttpPost("CreateRoom")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            var result = await _roomService.CreateRoomAsync(roomDto);
            return Ok(result);
        }

        [HttpDelete("DeleteRoom")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> DeleteRoom(int roomId)
        {
            var result = await _roomService.DeleteRoomHardAsync(roomId);
            return Ok(result);
        }

        [HttpPut("UpdateRoom")]
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> UpdateRoom([FromBody] UpdateRoomDto roomDto)
        {
            var result = await _roomService.UpdateRoomAsync(roomDto);
            return Ok(result);
        }

        [HttpGet("ViewAllRooms")]
        public async Task<ActionResult> GetAllRooms(int? pageSize = 5, int? pageIndex = 1)
        {
            var result = await _roomService.GetAllRoomsAsync(pageSize, pageIndex);
            return Ok(result);
        }
    }
}
