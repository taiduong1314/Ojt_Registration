using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Updatetblcompanymajor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_company_MajorCompanies_MajorCompanyId",
                table: "tbl_company");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_major_MajorCompanies_MajorCompanyId",
                table: "tbl_major");

            migrationBuilder.RenameColumn(
                name: "MajorCompanyId",
                table: "tbl_major",
                newName: "majorCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_major_MajorCompanyId",
                table: "tbl_major",
                newName: "IX_tbl_major_majorCompanyId");

            migrationBuilder.RenameColumn(
                name: "MajorCompanyId",
                table: "tbl_company",
                newName: "majorCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_company_MajorCompanyId",
                table: "tbl_company",
                newName: "IX_tbl_company_majorCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_company_MajorCompanies_majorCompanyId",
                table: "tbl_company",
                column: "majorCompanyId",
                principalTable: "MajorCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_major_MajorCompanies_majorCompanyId",
                table: "tbl_major",
                column: "majorCompanyId",
                principalTable: "MajorCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_company_MajorCompanies_majorCompanyId",
                table: "tbl_company");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_major_MajorCompanies_majorCompanyId",
                table: "tbl_major");

            migrationBuilder.RenameColumn(
                name: "majorCompanyId",
                table: "tbl_major",
                newName: "MajorCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_major_majorCompanyId",
                table: "tbl_major",
                newName: "IX_tbl_major_MajorCompanyId");

            migrationBuilder.RenameColumn(
                name: "majorCompanyId",
                table: "tbl_company",
                newName: "MajorCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_company_majorCompanyId",
                table: "tbl_company",
                newName: "IX_tbl_company_MajorCompanyId");

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
    }
}
