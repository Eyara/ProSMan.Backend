using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class AddedFinishedOnToTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedOn",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedOn",
                table: "NonSprintTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedOn",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FinishedOn",
                table: "NonSprintTasks");
        }
    }
}
