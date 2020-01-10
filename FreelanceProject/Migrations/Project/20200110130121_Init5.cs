using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "JobsFreelancers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers",
                columns: new[] { "FreelancerId", "JobId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "JobsFreelancers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers",
                columns: new[] { "FreelancerId", "JobId", "Status" });
        }
    }
}
