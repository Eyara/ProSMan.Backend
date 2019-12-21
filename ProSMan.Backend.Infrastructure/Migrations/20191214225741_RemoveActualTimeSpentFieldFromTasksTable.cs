using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class RemoveActualTimeSpentFieldFromTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSpentTime",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActualSpentTime",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
