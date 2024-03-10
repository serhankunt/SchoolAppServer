using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTierArchitecture.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassRooms_ClassroomId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "Students",
                newName: "ClassRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                newName: "IX_Students_ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassRooms_ClassRoomId",
                table: "Students",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassRooms_ClassRoomId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ClassRoomId",
                table: "Students",
                newName: "ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassRoomId",
                table: "Students",
                newName: "IX_Students_ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassRooms_ClassroomId",
                table: "Students",
                column: "ClassroomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
