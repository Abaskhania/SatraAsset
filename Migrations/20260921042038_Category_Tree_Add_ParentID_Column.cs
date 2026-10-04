using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatraAsset.Migrations
{
    /// <inheritdoc />
    public partial class Category_Tree_Add_ParentID_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetRecipient_Personel_PersonelId",
                table: "AssetRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Personel_ServiceLocations_LocationId",
                table: "Personel");

            migrationBuilder.DropForeignKey(
                name: "FK_SatraUser_Personel_PersonelID",
                table: "SatraUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personel",
                table: "Personel");

            migrationBuilder.RenameTable(
                name: "Personel",
                newName: "Personels");

            migrationBuilder.RenameIndex(
                name: "IX_Personel_LocationId",
                table: "Personels",
                newName: "IX_Personels_LocationId");

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NCD",
                table: "Personels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personels",
                table: "Personels",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentID",
                table: "Categories",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRecipient_Personels_PersonelId",
                table: "AssetRecipient",
                column: "PersonelId",
                principalTable: "Personels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentID",
                table: "Categories",
                column: "ParentID",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_ServiceLocations_LocationId",
                table: "Personels",
                column: "LocationId",
                principalTable: "ServiceLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SatraUser_Personels_PersonelID",
                table: "SatraUser",
                column: "PersonelID",
                principalTable: "Personels",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetRecipient_Personels_PersonelId",
                table: "AssetRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Personels_ServiceLocations_LocationId",
                table: "Personels");

            migrationBuilder.DropForeignKey(
                name: "FK_SatraUser_Personels_PersonelID",
                table: "SatraUser");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentID",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personels",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NCD",
                table: "Personels");

            migrationBuilder.RenameTable(
                name: "Personels",
                newName: "Personel");

            migrationBuilder.RenameIndex(
                name: "IX_Personels_LocationId",
                table: "Personel",
                newName: "IX_Personel_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personel",
                table: "Personel",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRecipient_Personel_PersonelId",
                table: "AssetRecipient",
                column: "PersonelId",
                principalTable: "Personel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personel_ServiceLocations_LocationId",
                table: "Personel",
                column: "LocationId",
                principalTable: "ServiceLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SatraUser_Personel_PersonelID",
                table: "SatraUser",
                column: "PersonelID",
                principalTable: "Personel",
                principalColumn: "ID");
        }
    }
}
