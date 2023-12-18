using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Editpropertyofapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cv",
                table: "tbl_recruiment_apply",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(MAX)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "cv",
                table: "tbl_recruiment_apply",
                type: "varbinary(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");
        }
    }
}
