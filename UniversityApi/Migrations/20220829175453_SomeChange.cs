using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.Migrations
{
    public partial class SomeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_Subjects_subjectsId",
                table: "GroupSubject");

            migrationBuilder.RenameColumn(
                name: "subjectsId",
                table: "GroupSubject",
                newName: "SubjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubject_subjectsId",
                table: "GroupSubject",
                newName: "IX_GroupSubject_SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_Subjects_SubjectsId",
                table: "GroupSubject",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubject_Subjects_SubjectsId",
                table: "GroupSubject");

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "GroupSubject",
                newName: "subjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubject_SubjectsId",
                table: "GroupSubject",
                newName: "IX_GroupSubject_subjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubject_Subjects_subjectsId",
                table: "GroupSubject",
                column: "subjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
