using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreToInterviewQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "InterviewQuestions",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Score",
                value: null);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Score",
                value: null);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Score",
                value: null);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Score",
                value: null);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Score",
                value: null);

            migrationBuilder.UpdateData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Score",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "InterviewQuestions");
        }
    }
}
