using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleLemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class fix_table_relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TableBookings_BookingTimesId",
                table: "TableBookings");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_BookingTimesId",
                table: "TableBookings",
                column: "BookingTimesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TableBookings_BookingTimesId",
                table: "TableBookings");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_BookingTimesId",
                table: "TableBookings",
                column: "BookingTimesId",
                unique: true);
        }
    }
}
