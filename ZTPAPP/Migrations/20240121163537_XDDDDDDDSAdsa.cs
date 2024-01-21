using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZTPAPP.Migrations
{
    /// <inheritdoc />
    public partial class XDDDDDDDSAdsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardSets_Tests_TestId",
                table: "FlashcardSets");

            migrationBuilder.DropIndex(
                name: "IX_FlashcardSets_TestId",
                table: "FlashcardSets");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "FlashcardSets");

            migrationBuilder.CreateTable(
                name: "FlashcardSetTest",
                columns: table => new
                {
                    FlashcardSetsId = table.Column<int>(type: "int", nullable: false),
                    TestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardSetTest", x => new { x.FlashcardSetsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_FlashcardSetTest_FlashcardSets_FlashcardSetsId",
                        column: x => x.FlashcardSetsId,
                        principalTable: "FlashcardSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashcardSetTest_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSetTest_TestsId",
                table: "FlashcardSetTest",
                column: "TestsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardSetTest");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "FlashcardSets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSets_TestId",
                table: "FlashcardSets",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardSets_Tests_TestId",
                table: "FlashcardSets",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
