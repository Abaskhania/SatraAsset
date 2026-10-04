using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatraAsset.Migrations
{
    /// <inheritdoc />
    public partial class VerifAmval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerif",
                table: "AssetRecipient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerifDate",
                table: "AssetRecipient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VerifUser",
                table: "AssetRecipient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerif",
                table: "AssetRecipient");

            migrationBuilder.DropColumn(
                name: "VerifDate",
                table: "AssetRecipient");

            migrationBuilder.DropColumn(
                name: "VerifUser",
                table: "AssetRecipient");
        }
    }
}
