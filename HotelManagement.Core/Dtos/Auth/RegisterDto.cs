using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="UserName is Required")]
        [MaxLength(50,ErrorMessage ="Max Length is 50")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        [MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "User Type is Required")]
        public string UserType { get; set; }

        public string? EmploymentType { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

    }
}
