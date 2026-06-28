using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAudioInHrQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioQuestion",
                table: "HrQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 1,
                column: "AudioQuestion",
                value: null);

            migrationBuilder.UpdateData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 2,
                column: "AudioQuestion",
                value: null);

            migrationBuilder.UpdateData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 3,
                column: "AudioQuestion",
                value: null);

            migrationBuilder.UpdateData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "AudioQuestion",
                value: null);

            migrationBuilder.UpdateData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 5,
                column: "AudioQuestion",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioQuestion",
                table: "HrQuestions");
        }
    }
}
