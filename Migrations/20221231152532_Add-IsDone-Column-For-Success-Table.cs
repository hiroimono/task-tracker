using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_tracker.Migrations
{
    public partial class AddIsDoneColumnForSuccessTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Successes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Successes");
        }
    }
}
