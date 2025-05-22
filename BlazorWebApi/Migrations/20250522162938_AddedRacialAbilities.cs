using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedRacialAbilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RacialAbilities",
                table: "CharacterRaces",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RacialAbilities",
                table: "CharacterRaces");
        }
    }
}
