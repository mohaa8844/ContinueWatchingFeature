using Microsoft.EntityFrameworkCore.Migrations;

namespace ContinueWatchingFeature.Migrations
{
    public partial class addseasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seasons",
                table: "Series",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seasons",
                table: "Series");
        }
    }
}
