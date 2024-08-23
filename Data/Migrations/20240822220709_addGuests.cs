using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_management_asp_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "tblGuests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblGuests_EventId",
                table: "tblGuests",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblGuests_tblEvents_EventId",
                table: "tblGuests",
                column: "EventId",
                principalTable: "tblEvents",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGuests_tblEvents_EventId",
                table: "tblGuests");

            migrationBuilder.DropIndex(
                name: "IX_tblGuests_EventId",
                table: "tblGuests");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "tblGuests");
        }
    }
}
