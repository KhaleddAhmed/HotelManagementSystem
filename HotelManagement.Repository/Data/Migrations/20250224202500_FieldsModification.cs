using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldsModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraPrivilege",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomView",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsSea",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSea",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "ExtraPrivilege",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomView",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
