using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PersonRecords_PersonId",
                table: "PersonRecords",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRecords_RoomId",
                table: "PersonRecords",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRecords_Persons_PersonId",
                table: "PersonRecords",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRecords_Rooms_RoomId",
                table: "PersonRecords",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRecords_Persons_PersonId",
                table: "PersonRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonRecords_Rooms_RoomId",
                table: "PersonRecords");

            migrationBuilder.DropIndex(
                name: "IX_PersonRecords_PersonId",
                table: "PersonRecords");

            migrationBuilder.DropIndex(
                name: "IX_PersonRecords_RoomId",
                table: "PersonRecords");
        }
    }
}
