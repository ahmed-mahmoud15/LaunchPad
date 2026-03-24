using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLeetCodeIdToCodingQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeetcodeId",
                table: "CodingQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "LeetcodeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "LeetcodeId",
                value: 206);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "LeetcodeId",
                value: 102);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "LeetcodeId",
                value: 11143);

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "LeetcodeId",
                value: 23);

            migrationBuilder.CreateIndex(
                name: "IX_CodingQuestions_LeetcodeId",
                table: "CodingQuestions",
                column: "LeetcodeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CodingQuestions_LeetcodeId",
                table: "CodingQuestions");

            migrationBuilder.DropColumn(
                name: "LeetcodeId",
                table: "CodingQuestions");
        }
    }
}
