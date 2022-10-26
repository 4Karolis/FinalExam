using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DAL.Migrations
{
    public partial class AddedProfilePic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "PersonalInfoId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PersonalInfoId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PersonalInfoId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
