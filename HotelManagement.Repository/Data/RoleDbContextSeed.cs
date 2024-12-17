using HotelManagement.Core.Entities.Identity;
using HotelManagement.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Repository.Data
{
    public static class RoleDbContextSeed
    {
        public async static Task SeedRoleAsync(RoleManager<IdentityRole> _roleManager)
        {
            if(_roleManager.Roles.Count()==0)
            {
                var role=new IdentityRole()
                {
                    Name="Staff"
                };

                var userRole = new IdentityRole()
                {
                    Name = "User"
                };

                await _roleManager.CreateAsync(role);
                await _roleManager.CreateAsync(userRole);
            }
        }
    }
}
