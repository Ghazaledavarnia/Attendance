using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication2.Data.Migrations
{
    public partial class addcolapplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_User_AplicationUsersId",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "AplicationUsersId",
                table: "Leave",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_AplicationUsersId",
                table: "Leave",
                newName: "IX_Leave_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_User_ApplicationUserId",
                table: "Leave",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_User_ApplicationUserId",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Leave",
                newName: "AplicationUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_ApplicationUserId",
                table: "Leave",
                newName: "IX_Leave_AplicationUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_User_AplicationUsersId",
                table: "Leave",
                column: "AplicationUsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
