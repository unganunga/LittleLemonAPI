using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleLemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class format : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableBookings_Bookings_BookingTimesId",
                table: "TableBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "BookingTimes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingTimes",
                table: "BookingTimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableBookings_BookingTimes_BookingTimesId",
                table: "TableBookings",
                column: "BookingTimesId",
                principalTable: "BookingTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableBookings_BookingTimes_BookingTimesId",
                table: "TableBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingTimes",
                table: "BookingTimes");

            migrationBuilder.RenameTable(
                name: "BookingTimes",
                newName: "Bookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableBookings_Bookings_BookingTimesId",
                table: "TableBookings",
                column: "BookingTimesId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
