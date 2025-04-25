using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Service;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IFacilitiesService
    {
        Task<GenericResponse<bool>> CreateServiceAsync(CreateServiecDto createServiecDto);
        Task<GenericResponse<bool>> DeleteServiceAsync(int serviceId);
        Task<GenericResponse<bool>> UpdateServiceAsync(UpdateServiceDto updateServiceDto);

        Task<GenericResponse<List<GetAllServiceDto>>> GetAllServicesAsync();

        Task<GenericResponse<GetServicDetails>> GetServiceDetailsAsync(int serviceId);

        Task<GenericResponse<List<UserServiceApprovementDto>>> GetAllUsersServices();

        Task<GenericResponse<bool>> ApproveOnServicesAsync(int serviceId, int guestId);
        Task<GenericResponse<bool>> RejectOnServicesAsync(int serviceId, int guestId);
    }
}
