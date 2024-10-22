using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementRepository.Migrations
{
    /// <inheritdoc />
    public partial class addmomid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubCourses_Students_StudentId",
                table: "StudentSubCourses");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentSubCourses",
                newName: "MomId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubCourses_StudentId",
                table: "StudentSubCourses",
                newName: "IX_StudentSubCourses_MomId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubCourses_Students_MomId",
                table: "StudentSubCourses",
                column: "MomId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubCourses_Students_MomId",
                table: "StudentSubCourses");

            migrationBuilder.RenameColumn(
                name: "MomId",
                table: "StudentSubCourses",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubCourses_MomId",
                table: "StudentSubCourses",
                newName: "IX_StudentSubCourses_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubCourses_Students_StudentId",
                table: "StudentSubCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
