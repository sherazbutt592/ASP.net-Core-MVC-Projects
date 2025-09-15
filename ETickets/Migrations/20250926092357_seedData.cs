using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FullName", "ProfilePicture" },
                values: new object[,]
                {
                    { 1, "Action star", "Tom", "actor1.jpg" },
                    { 2, "Comedy Star", "Jack", "actor2.jpg" },
                    { 3, "Mystery Start", "John", "actor3.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Description", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "Main city cinema", "cinema1.jpg", "Cinema One" },
                    { 2, "Suburb cinema", "cinema2.jpg", "Cinema Two" },
                    { 3, "New cinema", "cinema3.jpg", "Cinema Three" }
                });

            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Bio", "FullName", "ProfilePicture" },
                values: new object[,]
                {
                    { 1, "Award-winning producer", "John Producer", "producer1.jpg" },
                    { 2, "Indie film expert", "Jane Producer", "producer2.jpg" },
                    { 3, "CGI film expert", "Joy Producer", "producer3.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CinemaId", "Description", "EndDate", "Image", "MovieCategory", "Name", "Price", "ProducerId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "An exciting action movie.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie1.jpg", 0, "Action Movie", 12.99, 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "A hilarious comedy.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie2.jpg", 2, "Comedy Movie", 10.99, 2, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "A Masterpiece Mystery.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie3.jpg", 4, "Mystery Movie", 10.99, 3, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
