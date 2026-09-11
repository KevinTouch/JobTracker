using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobTracker.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJobDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "JobApplications",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "JobApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "InterviewDate",
                table: "JobApplications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobApplications",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "NextActionDate",
                table: "JobApplications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryRange",
                table: "JobApplications",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WorkArrangement",
                table: "JobApplications",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_NextActionDate",
                table: "JobApplications",
                column: "NextActionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_NextActionDate",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "NextActionDate",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "SalaryRange",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "WorkArrangement",
                table: "JobApplications");
        }
    }
}
