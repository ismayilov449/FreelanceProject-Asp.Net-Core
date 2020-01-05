using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobFreelancer",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false),
                    FreelancerId = table.Column<int>(nullable: false),
                    FreelancerId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFreelancer", x => new { x.FreelancerId, x.JobId });
                    table.ForeignKey(
                        name: "FK_JobFreelancer_Freelancers_FreelancerId1",
                        column: x => x.FreelancerId1,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobFreelancer_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobFreelancer_FreelancerId1",
                table: "JobFreelancer",
                column: "FreelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobFreelancer_JobId",
                table: "JobFreelancer",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobFreelancer");
        }
    }
}
