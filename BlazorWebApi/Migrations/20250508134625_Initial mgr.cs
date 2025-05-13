using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initialmgr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WowClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WowClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterRaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterRaces_Factions_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WowClassId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specializations_WowClasses_WowClassId",
                        column: x => x.WowClassId,
                        principalTable: "WowClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceWowClasses",
                columns: table => new
                {
                    CharacterRaceId = table.Column<int>(type: "int", nullable: false),
                    WowClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceWowClasses", x => new { x.CharacterRaceId, x.WowClassId });
                    table.ForeignKey(
                        name: "FK_RaceWowClasses_CharacterRaces_CharacterRaceId",
                        column: x => x.CharacterRaceId,
                        principalTable: "CharacterRaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceWowClasses_WowClasses_WowClassId",
                        column: x => x.WowClassId,
                        principalTable: "WowClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterRaces_FactionId",
                table: "CharacterRaces",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceWowClasses_WowClassId",
                table: "RaceWowClasses",
                column: "WowClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_RoleId",
                table: "Specializations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_WowClassId",
                table: "Specializations",
                column: "WowClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceWowClasses");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "CharacterRaces");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "WowClasses");

            migrationBuilder.DropTable(
                name: "Factions");
        }
    }
}
