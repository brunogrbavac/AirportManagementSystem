using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManagementWebAPI.Migrations
{
    public partial class AddedCarrierIdToFlightsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CarrierId",
                table: "Flights",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Carriers_CarrierId",
                table: "Flights",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Carriers_CarrierId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_CarrierId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Flights");
        }
    }
}
