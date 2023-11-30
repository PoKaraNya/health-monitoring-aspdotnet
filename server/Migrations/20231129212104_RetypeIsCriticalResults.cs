using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RetypeIsCriticalResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCriticalResults",
                table: "RoomRecords");

            migrationBuilder.DropColumn(
                name: "IsCriticalResults",
                table: "PersonRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IsCriticalResults",
                table: "RoomRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "IsCriticalResults",
                table: "PersonRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
