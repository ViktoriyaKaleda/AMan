using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMan.Migrations
{
    public partial class ChangeJobFieldAndroid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentJobId",
                table: "Android",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android",
                column: "CurrentJobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentJobId",
                table: "Android",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android",
                column: "CurrentJobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
