using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication2.Data.Migrations
{
    public partial class leaveclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AplicationUsersId = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    LeavesId = table.Column<int>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leave_User_AplicationUsersId",
                        column: x => x.AplicationUsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leave_Leave_LeavesId",
                        column: x => x.LeavesId,
                        principalTable: "Leave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leave_AplicationUsersId",
                table: "Leave",
                column: "AplicationUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeavesId",
                table: "Leave",
                column: "LeavesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leave");
        }
    }
}
