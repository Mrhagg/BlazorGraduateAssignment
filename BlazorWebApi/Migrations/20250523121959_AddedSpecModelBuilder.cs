using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolesId",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_RolesId",
                table: "Specializations",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Roles_RolesId",
                table: "Specializations",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Roles_RolesId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_RolesId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "Specializations");
        }
    }
}
