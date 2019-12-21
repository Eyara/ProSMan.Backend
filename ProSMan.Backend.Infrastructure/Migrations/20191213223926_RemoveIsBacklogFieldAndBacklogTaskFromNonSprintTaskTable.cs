using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class RemoveIsBacklogFieldAndBacklogTaskFromNonSprintTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
				DELETE from NonSprintTasks
					WHERE IsBacklog = 1
			");
            migrationBuilder.DropColumn(
                name: "IsBacklog",
                table: "NonSprintTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBacklog",
                table: "NonSprintTasks",
                nullable: false,
                defaultValue: false);
        }
    }
}
