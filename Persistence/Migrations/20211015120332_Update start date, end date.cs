using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Updatestartdateenddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "tbl_recruiment_apply");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "tbl_ojt_report");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "tbl_ojt_report");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "tbl_student",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "tbl_student",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingStatus",
                table: "tbl_student",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "tbl_student");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "tbl_student");

            migrationBuilder.DropColumn(
                name: "WorkingStatus",
                table: "tbl_student");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "tbl_recruiment_apply",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "tbl_ojt_report",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date",
                table: "tbl_ojt_report",
                type: "datetime",
                nullable: true);
        }
    }
}
