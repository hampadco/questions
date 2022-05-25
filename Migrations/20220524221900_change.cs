using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dr.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbl_Answers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tbl_Answers");

            migrationBuilder.DropColumn(
                name: "state",
                table: "tbl_Answers");

            migrationBuilder.RenameColumn(
                name: "idquestion",
                table: "tbl_Answers",
                newName: "idjoin");

            migrationBuilder.RenameColumn(
                name: "Doctor",
                table: "tbl_Answers",
                newName: "IdUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "tbl_Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idjoin",
                table: "tbl_Answers",
                newName: "idquestion");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "tbl_Answers",
                newName: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "create_date",
                table: "tbl_Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbl_Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tbl_Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "state",
                table: "tbl_Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
