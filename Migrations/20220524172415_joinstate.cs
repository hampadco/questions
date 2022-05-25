using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dr.Migrations
{
    public partial class joinstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "tbl_Joins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tbl_Joins");
        }
    }
}
