using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0302bfae-da03-404e-9213-f5e55919d17d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3f63b99-1fd7-43ce-a58a-820ed1ca670f");

            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3342828e-e968-4cb3-b135-52465e4666cc", null, "Admin", "ADMIN" },
                    { "7a3e8cd5-a243-4529-88d4-5a2cd6607e1d", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_appUserId",
                table: "Comments",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_appUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3342828e-e968-4cb3-b135-52465e4666cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a3e8cd5-a243-4529-88d4-5a2cd6607e1d");

            migrationBuilder.DropColumn(
                name: "AppUser",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0302bfae-da03-404e-9213-f5e55919d17d", null, "User", "USER" },
                    { "a3f63b99-1fd7-43ce-a58a-820ed1ca670f", null, "Admin", "ADMIN" }
                });
        }
    }
}
