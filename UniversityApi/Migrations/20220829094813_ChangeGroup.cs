using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.Migrations
{
    public partial class ChangeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Groups",
                newName: "Section");

            migrationBuilder.AddColumn<string>(
                name: "GradeName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeName",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Section",
                table: "Groups",
                newName: "Specialization");
        }
    }
}
