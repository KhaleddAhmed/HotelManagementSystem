using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Dtos.Checkout;
using HotelManagement.Core.Responses;

namespace HotelManagement.Core.Service.Contract
{
    public interface IGuestService
    {
        Task<GenericResponse<GetCheckoutDto>> CheckOutAsync(int roomId, string userId);
    }
}
