using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 20, 10, 44, 901, DateTimeKind.Local).AddTicks(3240), new DateTime(2025, 9, 19, 20, 10, 44, 899, DateTimeKind.Local).AddTicks(7878) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 20, 10, 44, 901, DateTimeKind.Local).AddTicks(3732), new DateTime(2025, 9, 19, 20, 10, 44, 901, DateTimeKind.Local).AddTicks(3729) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 26, 20, 10, 44, 901, DateTimeKind.Local).AddTicks(3739), new DateTime(2025, 9, 19, 20, 10, 44, 901, DateTimeKind.Local).AddTicks(3738) });
        }
    }
}
