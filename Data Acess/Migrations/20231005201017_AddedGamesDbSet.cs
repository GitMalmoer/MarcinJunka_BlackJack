using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class AddedGamesDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "PlacedBets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameResolutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacedBets_GameId",
                table: "PlacedBets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResolutions_GameId",
                table: "GameResolutions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameResolutions_Games_GameId",
                table: "GameResolutions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacedBets_Games_GameId",
                table: "PlacedBets",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameResolutions_Games_GameId",
                table: "GameResolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacedBets_Games_GameId",
                table: "PlacedBets");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_PlacedBets_GameId",
                table: "PlacedBets");

            migrationBuilder.DropIndex(
                name: "IX_GameResolutions_GameId",
                table: "GameResolutions");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "PlacedBets");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameResolutions");
        }
    }
}
