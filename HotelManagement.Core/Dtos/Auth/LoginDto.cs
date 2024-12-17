using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Auth
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage ="Invalid Email format")]
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
