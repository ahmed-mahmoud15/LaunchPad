using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToCodingQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleSlug",
                table: "CodingQuestions",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TitleSlug",
                value: "two-sum");

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TitleSlug",
                value: "reverse-linked-list");

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "TitleSlug",
                value: "binary-tree-level-order-traversal");

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "TitleSlug",
                value: "longest-common-subsequence");

            migrationBuilder.UpdateData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "TitleSlug",
                value: "merge-k-sorted-lists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleSlug",
                table: "CodingQuestions");
        }
    }
}
