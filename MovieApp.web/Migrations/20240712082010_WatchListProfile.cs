using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.web.Migrations
{
    public partial class WatchListProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WatchLaterMovies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WatchedMovies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaterMovies_MovieId",
                table: "WatchLaterMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_MovieId",
                table: "WatchedMovies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_Movies_MovieId",
                table: "WatchedMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterMovies_Movies_MovieId",
                table: "WatchLaterMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_Movies_MovieId",
                table: "WatchedMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterMovies_Movies_MovieId",
                table: "WatchLaterMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchLaterMovies_MovieId",
                table: "WatchLaterMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchedMovies_MovieId",
                table: "WatchedMovies");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WatchLaterMovies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WatchedMovies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
