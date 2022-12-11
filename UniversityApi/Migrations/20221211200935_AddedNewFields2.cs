using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.Migrations
{
    public partial class AddedNewFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Section",
                table: "Groups",
                newName: "Department");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentID",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Groups",
                newName: "Section");
        }
    }
}
