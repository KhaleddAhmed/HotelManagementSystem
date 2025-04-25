using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Repository.Data.Configurations
{
    internal class GuestServiceConfigurations : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder.HasKey(US => new { US.GuestId, US.ServiceId });

            builder
                .HasOne(US => US.Service)
                .WithMany(S => S.UserServices)
                .HasForeignKey(US => US.ServiceId);

            builder
                .HasOne(US => US.Guest)
                .WithMany(G => G.UserServices)
                .HasForeignKey(US => US.GuestId);
        }
    }
}
