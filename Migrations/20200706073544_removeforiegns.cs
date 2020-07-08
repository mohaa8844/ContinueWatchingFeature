using Microsoft.EntityFrameworkCore.Migrations;

namespace ContinueWatchingFeature.Migrations
{
    public partial class removeforiegns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epsoides_Series_SeriesId",
                table: "Epsoides");

            migrationBuilder.DropForeignKey(
                name: "FK_Still_Watchings_Users_UserId",
                table: "Still_Watchings");

            migrationBuilder.DropIndex(
                name: "IX_Still_Watchings_UserId",
                table: "Still_Watchings");

            migrationBuilder.DropIndex(
                name: "IX_Epsoides_SeriesId",
                table: "Epsoides");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Still_Watchings");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Epsoides");

            migrationBuilder.AddColumn<int>(
                name: "User_id",
                table: "Still_Watchings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Series_Id",
                table: "Epsoides",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_id",
                table: "Still_Watchings");

            migrationBuilder.DropColumn(
                name: "Series_Id",
                table: "Epsoides");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Still_Watchings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "Epsoides",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Still_Watchings_UserId",
                table: "Still_Watchings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Epsoides_SeriesId",
                table: "Epsoides",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epsoides_Series_SeriesId",
                table: "Epsoides",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Still_Watchings_Users_UserId",
                table: "Still_Watchings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
