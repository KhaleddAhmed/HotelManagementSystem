using HotelManagement.Core;
using HotelManagement.Core.Dtos.Auth;
using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Entities.Identity;
using HotelManagement.Core.Service.Contract;
using HotelManagement.Service.Services.Token;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Service.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
           _signInManager = signInManager;
           _tokenService = tokenService;
          _unitOfWork = unitOfWork;
        }
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return null;
            return new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExistAsync(registerDto.Email))
                return null;
            
            var user = new AppUser()
            {
                Email = registerDto.Email,
                Address=registerDto.Address,
                Gender= registerDto.Gender,
                UserName = registerDto.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (registerDto.UserType=="Staff")
            {
                await _userManager.AddToRoleAsync(user, "Staff");
                var staff = new Staff()
                {
                    AppUserId = user.Id,
                    User = user,
                    EmploymentType = registerDto.EmploymentType
                };

                await _unitOfWork.Repository<Staff,string>().AddAsync(staff);
                await _unitOfWork.CompleteAsync();

            }
            else
                await _userManager.AddToRoleAsync(user, "User");



            if (!result.Succeeded)
                return null;
            return new UserDto()
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }
    }
}
