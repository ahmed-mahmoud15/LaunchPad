using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingQuestions_QuestionTopics_TopicId",
                table: "CodingQuestions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTopics_Name",
                table: "QuestionTopics");

            migrationBuilder.DropIndex(
                name: "IX_CodingQuestions_TopicId",
                table: "CodingQuestions");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "CodingQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "QuestionTopics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CodingQuestionTopics",
                columns: table => new
                {
                    CodingQuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingQuestionTopics", x => new { x.CodingQuestionId, x.QuestionTopicId });
                    table.ForeignKey(
                        name: "FK_CodingQuestionTopics_CodingQuestions_CodingQuestionId",
                        column: x => x.CodingQuestionId,
                        principalTable: "CodingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodingQuestionTopics_QuestionTopics_QuestionTopicId",
                        column: x => x.QuestionTopicId,
                        principalTable: "QuestionTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CodingQuestionTopics",
                columns: new[] { "CodingQuestionId", "QuestionTopicId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.UpdateData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 1,
                column: "Slug",
                value: "arrays-and-strings");

            migrationBuilder.UpdateData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 2,
                column: "Slug",
                value: "linked-list");

            migrationBuilder.UpdateData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 3,
                column: "Slug",
                value: "tree");

            migrationBuilder.UpdateData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 4,
                column: "Slug",
                value: "dynamic-programming");

            migrationBuilder.UpdateData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 5,
                column: "Slug",
                value: "sorting");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTopics_Slug",
                table: "QuestionTopics",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodingQuestionTopics_QuestionTopicId",
                table: "CodingQuestionTopics",
                column: "QuestionTopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodingQuestionTopics");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTopics_Slug",
                table: "QuestionTopics");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "QuestionTopics");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "CodingQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TopicId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TopicId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "TopicId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "TopicId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "TopicId",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTopics_Name",
                table: "QuestionTopics",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodingQuestions_TopicId",
                table: "CodingQuestions",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingQuestions_QuestionTopics_TopicId",
                table: "CodingQuestions",
                column: "TopicId",
                principalTable: "QuestionTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
