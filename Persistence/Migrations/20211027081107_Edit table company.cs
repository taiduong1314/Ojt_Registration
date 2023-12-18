using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Edittablecompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "tbl_company");

            migrationBuilder.DropColumn(
                name: "fullname",
                table: "tbl_company");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "tbl_company",
                newName: "HostManagerEmail");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "tbl_company_accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAccounts_CompanyId",
                table: "tbl_company_accounts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAccounts_tbl_company_CompanyId",
                table: "tbl_company_accounts",
                column: "CompanyId",
                principalTable: "tbl_company",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAccounts_tbl_company_CompanyId",
                table: "tbl_company_accounts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAccounts_CompanyId",
                table: "tbl_company_accounts");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tbl_company_accounts");

            migrationBuilder.RenameColumn(
                name: "HostManagerEmail",
                table: "tbl_company",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tbl_company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fullname",
                table: "tbl_company",
                type: "ntext",
                nullable: true);
        }
    }
}
