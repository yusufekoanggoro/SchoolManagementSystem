using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStudentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_students_user_id",
                table: "students");

            migrationBuilder.CreateIndex(
                name: "IX_students_user_id",
                table: "students",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_students_user_id",
                table: "students");

            migrationBuilder.CreateIndex(
                name: "IX_students_user_id",
                table: "students",
                column: "user_id");
        }
    }
}
