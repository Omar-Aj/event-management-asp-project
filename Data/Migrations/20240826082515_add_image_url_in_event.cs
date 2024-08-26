using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_management_asp_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_image_url_in_event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tblEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tblEvents");
        }
    }
}
