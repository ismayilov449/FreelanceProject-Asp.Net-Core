using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobFreelancer_Freelancers_FreelancerId1",
                table: "JobFreelancer");

            migrationBuilder.DropForeignKey(
                name: "FK_JobFreelancer_Jobs_JobId",
                table: "JobFreelancer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobFreelancer",
                table: "JobFreelancer");

            migrationBuilder.RenameTable(
                name: "JobFreelancer",
                newName: "JobsFreelancers");

            migrationBuilder.RenameIndex(
                name: "IX_JobFreelancer_JobId",
                table: "JobsFreelancers",
                newName: "IX_JobsFreelancers_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobFreelancer_FreelancerId1",
                table: "JobsFreelancers",
                newName: "IX_JobsFreelancers_FreelancerId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers",
                columns: new[] { "FreelancerId", "JobId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobsFreelancers_Freelancers_FreelancerId1",
                table: "JobsFreelancers",
                column: "FreelancerId1",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsFreelancers_Jobs_JobId",
                table: "JobsFreelancers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsFreelancers_Freelancers_FreelancerId1",
                table: "JobsFreelancers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsFreelancers_Jobs_JobId",
                table: "JobsFreelancers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsFreelancers",
                table: "JobsFreelancers");

            migrationBuilder.RenameTable(
                name: "JobsFreelancers",
                newName: "JobFreelancer");

            migrationBuilder.RenameIndex(
                name: "IX_JobsFreelancers_JobId",
                table: "JobFreelancer",
                newName: "IX_JobFreelancer_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobsFreelancers_FreelancerId1",
                table: "JobFreelancer",
                newName: "IX_JobFreelancer_FreelancerId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobFreelancer",
                table: "JobFreelancer",
                columns: new[] { "FreelancerId", "JobId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobFreelancer_Freelancers_FreelancerId1",
                table: "JobFreelancer",
                column: "FreelancerId1",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobFreelancer_Jobs_JobId",
                table: "JobFreelancer",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
