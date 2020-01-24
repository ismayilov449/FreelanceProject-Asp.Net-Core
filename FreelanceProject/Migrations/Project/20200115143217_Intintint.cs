using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceProject.Migrations.Project
{
    public partial class Intintint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobClient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    DateOfRequest = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobClient_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobClient_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobClient_ClientId",
                table: "JobClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_JobClient_JobId",
                table: "JobClient",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobClient");
        }
    }
}
