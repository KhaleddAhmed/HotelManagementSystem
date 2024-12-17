using HotelManagement.Core.Entities.Hotel;
using HotelManagement.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Repository.Data.Configurations
{
    internal class StaffConfigurations : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(S => S.EmploymentType).IsRequired().HasMaxLength(10);

            builder.HasOne(S => S.User)
                .WithOne()
                .HasForeignKey<Staff>(S => S.AppUserId);

            builder.HasKey(S => new{ S.AppUserId,S.EmploymentType});
        }
    }
}
