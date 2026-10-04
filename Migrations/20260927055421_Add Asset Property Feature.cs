using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatraAsset.Migrations
{
    /// <inheritdoc />
    public partial class AddAssetPropertyFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    OptionsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetProperties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetPropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetPropertyId = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetPropertyValues_AssetProperties_AssetPropertyId",
                        column: x => x.AssetPropertyId,
                        principalTable: "AssetProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetPropertyValues_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetProperties_CategoryId",
                table: "AssetProperties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValues_AssetId",
                table: "AssetPropertyValues",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValues_AssetPropertyId",
                table: "AssetPropertyValues",
                column: "AssetPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPropertyValues");

            migrationBuilder.DropTable(
                name: "AssetProperties");
        }
    }
}
