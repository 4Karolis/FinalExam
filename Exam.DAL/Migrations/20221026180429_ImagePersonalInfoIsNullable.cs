using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DAL.Migrations
{
    public partial class ImagePersonalInfoIsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PersonalInfoId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInfoId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PersonalInfoId",
                table: "Images",
                column: "PersonalInfoId",
                unique: true,
                filter: "[PersonalInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PersonalInfoId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInfoId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PersonalInfoId",
                table: "Images",
                column: "PersonalInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
