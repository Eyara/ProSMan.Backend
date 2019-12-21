using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
	public partial class FillBacklogTasks : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
				INSERT INTO BacklogTasks  
					SELECT Id, Name, Description, ProjectId FROM NonSprintTasks
					  WHERE IsBacklog = 1"
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			
		}
	}
}
