using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class AddedNonSprintTasksToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonSprintTask_Projects_ProjectId",
                table: "NonSprintTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NonSprintTask",
                table: "NonSprintTask");

            migrationBuilder.RenameTable(
                name: "NonSprintTask",
                newName: "NonSprintTasks");

            migrationBuilder.RenameIndex(
                name: "IX_NonSprintTask_ProjectId",
                table: "NonSprintTasks",
                newName: "IX_NonSprintTasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NonSprintTasks",
                table: "NonSprintTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NonSprintTasks_Projects_ProjectId",
                table: "NonSprintTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonSprintTasks_Projects_ProjectId",
                table: "NonSprintTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NonSprintTasks",
                table: "NonSprintTasks");

            migrationBuilder.RenameTable(
                name: "NonSprintTasks",
                newName: "NonSprintTask");

            migrationBuilder.RenameIndex(
                name: "IX_NonSprintTasks_ProjectId",
                table: "NonSprintTask",
                newName: "IX_NonSprintTask_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NonSprintTask",
                table: "NonSprintTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NonSprintTask_Projects_ProjectId",
                table: "NonSprintTask",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
