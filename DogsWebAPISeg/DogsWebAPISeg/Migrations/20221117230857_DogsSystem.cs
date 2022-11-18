using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogsWebAPISeg.Migrations
{
    /// <inheritdoc />
    public partial class DogsSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kennels_Kennels_KennelId",
                table: "Kennels");

            migrationBuilder.DropIndex(
                name: "IX_Kennels_KennelId",
                table: "Kennels");

            migrationBuilder.DropColumn(
                name: "KennelId",
                table: "Kennels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KennelId",
                table: "Kennels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kennels_KennelId",
                table: "Kennels",
                column: "KennelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kennels_Kennels_KennelId",
                table: "Kennels",
                column: "KennelId",
                principalTable: "Kennels",
                principalColumn: "Id");
        }
    }
}
