using Microsoft.EntityFrameworkCore.Migrations;

namespace ProSMan.Backend.Infrastructure.Migrations
{
	public partial class ChangedPriorityValueToInt : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
				UPDATE dbo.Tasks 
				SET
				  Priority = '0'
				WHERE
				  Priority = 'Low'
				GO
			");

			migrationBuilder.Sql(@"
				UPDATE dbo.Tasks 
				SET
				  Priority = '1'
				WHERE
				  Priority = 'Medium'
				GO
			");

			migrationBuilder.Sql(@"
				UPDATE dbo.Tasks 
				SET
				  Priority = '2'
				WHERE
				  Priority = 'High'
				GO
			");

			migrationBuilder.AlterColumn<int>(
				name: "Priority",
				table: "Tasks",
				nullable: false,
				oldClrType: typeof(string));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Priority",
				table: "Tasks",
				nullable: false,
				oldClrType: typeof(int));
		}
	}
}
