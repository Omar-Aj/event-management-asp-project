using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace event_management_asp_project.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactor_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "tblEvents");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "tblEvents");

            migrationBuilder.RenameColumn(
                name: "Services",
                table: "tblVenues",
                newName: "Service");

            migrationBuilder.AddColumn<int>(
                name: "DurationInHours",
                table: "tblEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "tblVenues",
                columns: new[] { "VenueId", "Capacity", "Location", "Name", "Service" },
                values: new object[,]
                {
                    { 1, 500, "Amman", "Mecca Mall", 2 },
                    { 2, 150, "Dead Sea", "Mariott Hotel", 0 },
                    { 3, 900, "Jerash", "Jerash Festival", 1 },
                    { 4, 50, "Amman", "FINC", 4 },
                    { 5, 999, "Aqaba", "Kempinski Hotel", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblVenues",
                keyColumn: "VenueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblVenues",
                keyColumn: "VenueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblVenues",
                keyColumn: "VenueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tblVenues",
                keyColumn: "VenueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tblVenues",
                keyColumn: "VenueId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "DurationInHours",
                table: "tblEvents");

            migrationBuilder.RenameColumn(
                name: "Service",
                table: "tblVenues",
                newName: "Services");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "tblEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "tblEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
