using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers");

            migrationBuilder.DropColumn(
                name: "JobStringId",
                table: "JobsFreelancers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JobsFreelancers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobsFreelancers");

            migrationBuilder.AddColumn<string>(
                name: "JobStringId",
                table: "JobsFreelancers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers",
                columns: new[] { "FreelancerId", "JobId" });
        }
    }
}
