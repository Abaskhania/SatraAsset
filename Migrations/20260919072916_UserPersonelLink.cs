using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatraAsset.Migrations
{
    /// <inheritdoc />
    public partial class UserPersonelLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonelID",
                table: "SatraUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnAt",
                table: "AssetRecipient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SatraUser_PersonelID",
                table: "SatraUser",
                column: "PersonelID");

            migrationBuilder.AddForeignKey(
                name: "FK_SatraUser_Personel_PersonelID",
                table: "SatraUser",
                column: "PersonelID",
                principalTable: "Personel",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatraUser_Personel_PersonelID",
                table: "SatraUser");

            migrationBuilder.DropIndex(
                name: "IX_SatraUser_PersonelID",
                table: "SatraUser");

            migrationBuilder.DropColumn(
                name: "PersonelID",
                table: "SatraUser");

            migrationBuilder.DropColumn(
                name: "ReturnAt",
                table: "AssetRecipient");
        }
    }
}
