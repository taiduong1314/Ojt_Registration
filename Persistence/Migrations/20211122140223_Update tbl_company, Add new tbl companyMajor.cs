using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Updatetbl_companyAddnewtblcompanyMajor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "MajorCompanyId",
                table: "tbl_major",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubCompany",
                table: "tbl_company",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MajorCompanyId",
                table: "tbl_company",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MajorCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorCompanies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_major_MajorCompanyId",
                table: "tbl_major",
                column: "MajorCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_company_MajorCompanyId",
                table: "tbl_company",
                column: "MajorCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_company_MajorCompanies_MajorCompanyId",
                table: "tbl_company",
                column: "MajorCompanyId",
                principalTable: "MajorCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_major_MajorCompanies_MajorCompanyId",
                table: "tbl_major",
                column: "MajorCompanyId",
                principalTable: "MajorCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_company_MajorCompanies_MajorCompanyId",
                table: "tbl_company");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_major_MajorCompanies_MajorCompanyId",
                table: "tbl_major");

            migrationBuilder.DropTable(
                name: "MajorCompanies");

            migrationBuilder.DropIndex(
                name: "IX_tbl_major_MajorCompanyId",
                table: "tbl_major");

            migrationBuilder.DropIndex(
                name: "IX_tbl_company_MajorCompanyId",
                table: "tbl_company");

            migrationBuilder.DropColumn(
                name: "MajorCompanyId",
                table: "tbl_major");

            migrationBuilder.DropColumn(
                name: "IsSubCompany",
                table: "tbl_company");

            migrationBuilder.DropColumn(
                name: "MajorCompanyId",
                table: "tbl_company");

        }
    }
}
