using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class isDeletedAddtoReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservation");
        }
    }
}
