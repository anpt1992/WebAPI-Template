using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Template.Migrations
{
    public partial class implementsoftdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_CreatedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Posts",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatedBy",
                table: "Posts",
                newName: "IX_Posts_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_CreatorId",
                table: "Posts",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_CreatorId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Posts",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatorId",
                table: "Posts",
                newName: "IX_Posts_CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_CreatedBy",
                table: "Posts",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
