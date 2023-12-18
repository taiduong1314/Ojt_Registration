using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Addnewdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_company",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "ntext", nullable: false),
                    address = table.Column<string>(type: "ntext", nullable: true),
                    web_site = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    fullname = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_fpt_staff",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    fullname = table.Column<string>(type: "ntext", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_fpt_staff", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_major",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    major_name = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_major", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_recruitment_information",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "ntext", nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    salary = table.Column<string>(type: "ntext", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    major_id = table.Column<int>(type: "int", nullable: true),
                    topic = table.Column<string>(type: "ntext", nullable: true),
                    area = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_recruitment_information", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_recruitment_information_tbl_company",
                        column: x => x.company_id,
                        principalTable: "tbl_company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phone = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    term = table.Column<int>(type: "int", nullable: true),
                    credit = table.Column<int>(type: "int", nullable: true),
                    gpa = table.Column<double>(type: "float", nullable: true),
                    IsPassCriteria = table.Column<bool>(type: "bit", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    major_id = table.Column<int>(type: "int", nullable: false),
                    student_code = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    email = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    fullname = table.Column<string>(type: "ntext", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_student_tbl_company",
                        column: x => x.company_id,
                        principalTable: "tbl_company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_student_tbl_major",
                        column: x => x.major_id,
                        principalTable: "tbl_major",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ojt_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    work_sort_description = table.Column<string>(type: "ntext", nullable: true),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    mark = table.Column<double>(type: "float", nullable: true),
                    on_work_date = table.Column<int>(type: "int", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    student_name = table.Column<string>(type: "ntext", nullable: true),
                    devision = table.Column<string>(type: "ntext", nullable: true),
                    line_manager_name = table.Column<string>(type: "ntext", nullable: true),
                    student_email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ojt_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_ojt_report_tbl_company",
                        column: x => x.company_id,
                        principalTable: "tbl_company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_ojt_report_tbl_student",
                        column: x => x.student_id,
                        principalTable: "tbl_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_recruiment_apply",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cv = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    recruiment_information_id = table.Column<int>(type: "int", nullable: true),
                    student_id = table.Column<int>(type: "int", nullable: true),
                    cover_letter = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_recruiment_apply", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_recruiment_apply_tbl_recruitment_information",
                        column: x => x.recruiment_information_id,
                        principalTable: "tbl_recruitment_information",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_recruiment_apply_tbl_student",
                        column: x => x.student_id,
                        principalTable: "tbl_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ojt_report_company_id",
                table: "tbl_ojt_report",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ojt_report_student_id",
                table: "tbl_ojt_report",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_recruiment_apply_recruiment_information_id",
                table: "tbl_recruiment_apply",
                column: "recruiment_information_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_recruiment_apply_student_id",
                table: "tbl_recruiment_apply",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_recruitment_information_company_id",
                table: "tbl_recruitment_information",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_student_company_id",
                table: "tbl_student",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_student_major_id",
                table: "tbl_student",
                column: "major_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_fpt_staff");

            migrationBuilder.DropTable(
                name: "tbl_ojt_report");

            migrationBuilder.DropTable(
                name: "tbl_recruiment_apply");

            migrationBuilder.DropTable(
                name: "tbl_recruitment_information");

            migrationBuilder.DropTable(
                name: "tbl_student");

            migrationBuilder.DropTable(
                name: "tbl_company");

            migrationBuilder.DropTable(
                name: "tbl_major");
        }
    }
}
