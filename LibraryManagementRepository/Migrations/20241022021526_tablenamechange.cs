using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementRepository.Migrations
{
    /// <inheritdoc />
    public partial class tablenamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubCourseAuditTrials",
                table: "StudentSubCourseAuditTrials");

            migrationBuilder.RenameTable(
                name: "StudentSubCourseAuditTrials",
                newName: "StudentSubCoursesAuditTrials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubCoursesAuditTrials",
                table: "StudentSubCoursesAuditTrials",
                column: "AuditTrialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubCoursesAuditTrials",
                table: "StudentSubCoursesAuditTrials");

            migrationBuilder.RenameTable(
                name: "StudentSubCoursesAuditTrials",
                newName: "StudentSubCourseAuditTrials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubCourseAuditTrials",
                table: "StudentSubCourseAuditTrials",
                column: "AuditTrialId");
        }
    }
}
