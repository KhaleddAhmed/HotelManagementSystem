using HotelManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Hotel
{
    public class Staff
    {
        public AppUser  User { get; set; }
        public string AppUserId { get; set; }
        public string EmploymentType { get; set; }



    }
}
