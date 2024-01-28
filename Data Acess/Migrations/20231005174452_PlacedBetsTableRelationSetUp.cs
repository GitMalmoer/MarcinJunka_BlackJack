using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class PlacedBetsTableRelationSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlacedBetId",
                table: "GameResolutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameResolutions_PlacedBetId",
                table: "GameResolutions",
                column: "PlacedBetId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameResolutions_PlacedBets_PlacedBetId",
                table: "GameResolutions",
                column: "PlacedBetId",
                principalTable: "PlacedBets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameResolutions_PlacedBets_PlacedBetId",
                table: "GameResolutions");

            migrationBuilder.DropIndex(
                name: "IX_GameResolutions_PlacedBetId",
                table: "GameResolutions");

            migrationBuilder.DropColumn(
                name: "PlacedBetId",
                table: "GameResolutions");
        }
    }
}
