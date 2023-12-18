using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class updatepositionofrecruitmentinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "tbl_recruiment_apply");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "tbl_recruitment_information",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "tbl_recruitment_information");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "tbl_recruiment_apply",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
