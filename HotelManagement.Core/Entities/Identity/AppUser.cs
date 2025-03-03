using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }
        public string Gender { get; set; }
        public Guest? Guest { get; set; }
    }
}
