using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTableAndFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classes_teachers_TeacherId",
                table: "classes");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_classes_ClassId",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_students_StudentId",
                table: "enrollments");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "enrollments",
                newName: "student_id");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "enrollments",
                newName: "class_id");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_StudentId_ClassId",
                table: "enrollments",
                newName: "IX_enrollments_student_id_class_id");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_ClassId",
                table: "enrollments",
                newName: "IX_enrollments_class_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "classes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "classes",
                newName: "teacher_id");

            migrationBuilder.RenameIndex(
                name: "IX_classes_TeacherId",
                table: "classes",
                newName: "IX_classes_teacher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_teachers_teacher_id",
                table: "classes",
                column: "teacher_id",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_classes_class_id",
                table: "enrollments",
                column: "class_id",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_students_student_id",
                table: "enrollments",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classes_teachers_teacher_id",
                table: "classes");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_classes_class_id",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_students_student_id",
                table: "enrollments");

            migrationBuilder.RenameColumn(
                name: "student_id",
                table: "enrollments",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "class_id",
                table: "enrollments",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_student_id_class_id",
                table: "enrollments",
                newName: "IX_enrollments_StudentId_ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_class_id",
                table: "enrollments",
                newName: "IX_enrollments_ClassId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "classes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "teacher_id",
                table: "classes",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_classes_teacher_id",
                table: "classes",
                newName: "IX_classes_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_teachers_TeacherId",
                table: "classes",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_classes_ClassId",
                table: "enrollments",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_students_StudentId",
                table: "enrollments",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
