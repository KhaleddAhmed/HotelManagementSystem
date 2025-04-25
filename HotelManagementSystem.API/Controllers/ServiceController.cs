using HotelManagement.Core.Dtos.Service;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{
    public class ServiceController : BaseApiController
    {
        private readonly IFacilitiesService _facilitiesService;

        public ServiceController(IFacilitiesService facilitiesService)
        {
            _facilitiesService = facilitiesService;
        }

        [Authorize(Roles = "Staff")]
        [HttpPost("CreateService")]
        public async Task<ActionResult> Create(CreateServiecDto createServiecDto)
        {
            var result = await _facilitiesService.CreateServiceAsync(createServiecDto);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("UpdateService")]
        public async Task<ActionResult> Update(UpdateServiceDto updateServiecDto)
        {
            var result = await _facilitiesService.UpdateServiceAsync(updateServiecDto);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpDelete("DeleteService")]
        public async Task<ActionResult> Delete(int serviceId)
        {
            var result = await _facilitiesService.DeleteServiceAsync(serviceId);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("GetAllServices")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _facilitiesService.GetAllServicesAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("GetServiceDetails")]
        public async Task<ActionResult> Get(int serviceId)
        {
            var result = await _facilitiesService.GetServiceDetailsAsync(serviceId);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("GetAllUsersServices")]
        public async Task<ActionResult> GetUserServices()
        {
            var result = await _facilitiesService.GetAllUsersServices();
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("ApproveUserService")]
        public async Task<ActionResult> Approve(int serviceId, int guestId)
        {
            var result = await _facilitiesService.ApproveOnServicesAsync(serviceId, guestId);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("RejectUserService")]
        public async Task<ActionResult> Reject(int serviceId, int guestId)
        {
            var result = await _facilitiesService.RejectOnServicesAsync(serviceId, guestId);
            return Ok(result);
        }
    }
}
