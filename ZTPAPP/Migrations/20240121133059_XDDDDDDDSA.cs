using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZTPAPP.Migrations
{
    /// <inheritdoc />
    public partial class XDDDDDDDSA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "FlashcardSets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestsHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestsHistories_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    FlashcardId = table.Column<int>(type: "int", nullable: true),
                    GivenAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Flashcards_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_TestsHistories_TestId",
                        column: x => x.TestId,
                        principalTable: "TestsHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSets_TestId",
                table: "FlashcardSets",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FlashcardId",
                table: "Answers",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestId",
                table: "Answers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsHistories_TestId",
                table: "TestsHistories",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardSets_Tests_TestId",
                table: "FlashcardSets",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardSets_Tests_TestId",
                table: "FlashcardSets");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "TestsHistories");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_FlashcardSets_TestId",
                table: "FlashcardSets");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "FlashcardSets");
        }
    }
}
