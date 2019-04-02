using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    public partial class AddedNonSprintTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonSprintTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TimeEstimate = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    IsBacklog = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonSprintTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonSprintTask_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonSprintTask_ProjectId",
                table: "NonSprintTask",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonSprintTask");
        }
    }
}
