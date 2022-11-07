using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DAL.Migrations
{
    public partial class AddedImageCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Images_PersonalInfos_PersonalInfoId",
                table: "Images",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id");
        }
    }
}
