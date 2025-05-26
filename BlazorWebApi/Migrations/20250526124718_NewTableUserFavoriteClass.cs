using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebApi.Migrations
{
    /// <inheritdoc />
    public partial class NewTableUserFavoriteClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteClass",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteClass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteClass", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteClass");
        }
    }
}
