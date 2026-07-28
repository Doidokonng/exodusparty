using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exodus_party.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumSpotifyTrackIdForYoutubeVideoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpotifyTrackId",
                table: "TrackHistories",
                newName: "YoutubeVideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YoutubeVideoId",
                table: "TrackHistories",
                newName: "SpotifyTrackId");
        }
    }
}
