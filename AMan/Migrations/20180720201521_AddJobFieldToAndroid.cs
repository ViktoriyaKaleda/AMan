using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMan.Migrations
{
    public partial class AddJobFieldToAndroid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentJobId",
                table: "Android",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Android_CurrentJobId",
                table: "Android",
                column: "CurrentJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android",
                column: "CurrentJobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Android_Job_CurrentJobId",
                table: "Android");

            migrationBuilder.DropIndex(
                name: "IX_Android_CurrentJobId",
                table: "Android");

            migrationBuilder.DropColumn(
                name: "CurrentJobId",
                table: "Android");
        }
    }
}
