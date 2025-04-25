using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Hotel
{
    public enum ServiceType
    {
        HouseKeeping = 1,
        RestrauntReserve,
        RequestMealOnRoom,
        other,
    }

    public class Service : BaseEntity
    {
        public ServiceType ServiceType { get; set; }

        public double ServiceFees { get; set; }

        public virtual ICollection<UserService> UserServices { get; set; }
    }
}
