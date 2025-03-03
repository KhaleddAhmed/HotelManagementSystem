using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Repository.Data.Configurations
{
    internal class GuestConfigurations : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder
                .HasOne(G => G.AppUser)
                .WithOne(u => u.Guest)
                .HasForeignKey<Guest>(G => G.AppUserId);
        }
    }
}
