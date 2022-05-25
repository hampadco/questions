using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dr.Migrations
{
    public partial class questions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idquestion = table.Column<int>(type: "int", nullable: false),
                    Idlistquestion = table.Column<int>(type: "int", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doctor = table.Column<int>(type: "int", nullable: false),
                    answer_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Answers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Listquestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idquestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title_question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Listquestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Questions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Answers");

            migrationBuilder.DropTable(
                name: "tbl_Listquestions");

            migrationBuilder.DropTable(
                name: "tbl_Questions");
        }
    }
}
