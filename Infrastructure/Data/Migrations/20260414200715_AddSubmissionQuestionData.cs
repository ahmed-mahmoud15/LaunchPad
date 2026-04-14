using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionQuestionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Memory",
                table: "AssessmentQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RunTime",
                table: "AssessmentQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestCasesPasses",
                table: "AssessmentQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalTestCases",
                table: "AssessmentQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Memory", "RunTime", "TestCasesPasses", "TotalTestCases" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Memory",
                table: "AssessmentQuestions");

            migrationBuilder.DropColumn(
                name: "RunTime",
                table: "AssessmentQuestions");

            migrationBuilder.DropColumn(
                name: "TestCasesPasses",
                table: "AssessmentQuestions");

            migrationBuilder.DropColumn(
                name: "TotalTestCases",
                table: "AssessmentQuestions");
        }
    }
}
