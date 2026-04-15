using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFromAndToStatusInHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ApplicationHistory",
                newName: "To");

            migrationBuilder.AddColumn<int>(
                name: "From",
                table: "ApplicationHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 1,
                column: "From",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "From", "To" },
                values: new object[] { 2, 3 });

            migrationBuilder.UpdateData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 3,
                column: "From",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 4,
                column: "From",
                value: 0);

            migrationBuilder.UpdateData(
                table: "JobTracks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentStatus",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "ApplicationHistory");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "ApplicationHistory",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "JobTracks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentStatus",
                value: 2);
        }
    }
}
