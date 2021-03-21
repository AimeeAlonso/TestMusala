using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveUniqueFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_GatewayId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GatewayId",
                table: "Devices",
                column: "GatewayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_GatewayId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GatewayId",
                table: "Devices",
                column: "GatewayId",
                unique: true);
        }
    }
}
