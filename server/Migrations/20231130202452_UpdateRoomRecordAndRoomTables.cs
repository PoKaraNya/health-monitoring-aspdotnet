using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomRecordAndRoomTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomRecords_RoomId",
                table: "RoomRecords",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRecords_Rooms_RoomId",
                table: "RoomRecords",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomRecords_Rooms_RoomId",
                table: "RoomRecords");

            migrationBuilder.DropIndex(
                name: "IX_RoomRecords_RoomId",
                table: "RoomRecords");
        }
    }
}
