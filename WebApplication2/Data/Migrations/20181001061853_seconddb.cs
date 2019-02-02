using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication2.Data.Migrations
{
    public partial class seconddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_LeaveType_LeaveTypeId",
                table: "Leave");

            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Leave_LeavesId",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_Leave_LeaveTypeId",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "LeavesId",
                table: "Leave",
                newName: "LeaveTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_LeavesId",
                table: "Leave",
                newName: "IX_Leave_LeaveTypeId1");

            migrationBuilder.AlterColumn<string>(
                name: "LeaveTypeId",
                table: "Leave",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_LeaveType_LeaveTypeId1",
                table: "Leave",
                column: "LeaveTypeId1",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_LeaveType_LeaveTypeId1",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "LeaveTypeId1",
                table: "Leave",
                newName: "LeavesId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_LeaveTypeId1",
                table: "Leave",
                newName: "IX_Leave_LeavesId");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "Leave",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeaveTypeId",
                table: "Leave",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_LeaveType_LeaveTypeId",
                table: "Leave",
                column: "LeaveTypeId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Leave_LeavesId",
                table: "Leave",
                column: "LeavesId",
                principalTable: "Leave",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
