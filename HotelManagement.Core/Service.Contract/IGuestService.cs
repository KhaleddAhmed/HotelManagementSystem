using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Checkout;
using HotelManagement.Core.Dtos.Service;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IGuestService
    {
        Task<GenericResponse<GetCheckoutDto>> CheckOutAsync(int roomId, string userId);

        Task<GenericResponse<List<GetAllAvailableServiceDto>>> GetAllAvailableServicesAsync();

        Task<GenericResponse<bool>> RequestService(string userId, int serviceId);
        Task<GenericResponse<List<GetAllRequestedServicesForUserDTO>>> GetAllServicesAsync(
            string userId
        );

        Task<GenericResponse<RequestedServiceDto>> GetRequestedServiceDetailsDto(
            string userId,
            int serviceId
        );
    }
}
