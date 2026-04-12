using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalyzeDateToCvJobAnaysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnalyzeDate",
                table: "CvJobAnalyses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 1,
                column: "AnalyzeDate",
                value: new DateTime(2026, 1, 15, 8, 30, 15, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 2,
                column: "AnalyzeDate",
                value: new DateTime(2026, 2, 15, 8, 30, 15, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 3,
                column: "AnalyzeDate",
                value: new DateTime(2026, 3, 15, 8, 30, 15, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalyzeDate",
                table: "CvJobAnalyses");
        }
    }
}
