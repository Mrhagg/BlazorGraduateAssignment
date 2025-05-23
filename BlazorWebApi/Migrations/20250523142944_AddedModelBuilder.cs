using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TalentTree_TalentTree_ParentId",
                table: "TalentTree");

            migrationBuilder.AddForeignKey(
                name: "FK_TalentTree_TalentTree_ParentId",
                table: "TalentTree",
                column: "ParentId",
                principalTable: "TalentTree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TalentTree_TalentTree_ParentId",
                table: "TalentTree");

            migrationBuilder.AddForeignKey(
                name: "FK_TalentTree_TalentTree_ParentId",
                table: "TalentTree",
                column: "ParentId",
                principalTable: "TalentTree",
                principalColumn: "Id");
        }
    }
}
