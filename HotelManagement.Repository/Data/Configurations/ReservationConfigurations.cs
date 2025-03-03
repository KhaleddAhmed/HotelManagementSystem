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
    internal class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(R => new { R.RoomId, R.GuestID });

            builder
                .HasOne(R => R.Guest)
                .WithMany(G => G.Reservations)
                .HasForeignKey(R => R.GuestID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(R => R.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(R => R.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
