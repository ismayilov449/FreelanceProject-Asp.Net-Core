using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class ModifyJobEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Jobs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Jobs");
        }
    }
}
