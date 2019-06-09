using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class RemovedToDateAndAddedFinishedOnFieldToSprintsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Sprints");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedOn",
                table: "Sprints",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedOn",
                table: "Sprints");

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Sprints",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
