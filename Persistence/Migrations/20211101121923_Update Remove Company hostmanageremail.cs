using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateRemoveCompanyhostmanageremail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostManagerEmail",
                table: "tbl_company");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AddColumn<string>(
                name: "HostManagerEmail",
                table: "tbl_company",
                type: "varchar(MAX)",
                nullable: true);

        }
    }
}
