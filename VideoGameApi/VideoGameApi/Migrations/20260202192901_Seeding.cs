using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameApi.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Platform",
                value: "Nintendo Switch");

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 2,
                column: "Platform",
                value: "PS4");

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 3,
                column: "Platform",
                value: "Multiple");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Platform",
                value: null);

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 2,
                column: "Platform",
                value: null);

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: 3,
                column: "Platform",
                value: null);
        }
    }
}
