using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class updatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FullName", "ProfilePicture" },
                values: new object[,]
                {
                    { 1, "This is the bio for the first actor", "Actor 1", "actor1.jpg" },
                    { 2, "This is the bio for the second actor", "Actor 2", "actor2.jpg" },
                    { 3, "This is the bio for the third actor", "Actor 3", "actor3.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Description", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "This is the description for the first cinema", "cinema1.jpg", "Cinema 1" },
                    { 2, "This is the description for the second cinema", "cinema3.jpg", "Cinema 2" },
                    { 3, "This is the description for the third cinema", "cinema3.jpg", "Cinema 3" }
                });

            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Bio", "FullName", "ProfilePicture" },
                values: new object[,]
                {
                    { 1, "This is the bio for the first Producer", "Producer 1", "producer1.jpg" },
                    { 2, "This is the bio for the second Producer", "Producer 2", "producer2.jpg" },
                    { 3, "This is the bio for the third Producer", "Producer 3", "producer3.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CinemaId", "Description", "EndDate", "Image", "MovieCategory", "Name", "Price", "ProducerId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "This is the description of first movie", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie1.jpg", 0, "Movie 1", 30.0, 2, new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "This is the description of second movie", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie2.jpg", 2, "Movie 2", 45.0, 2, new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "This is the description of third movie", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie3.jpg", 0, "Movie 3", 40.0, 3, new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 }
                });
        }
    }
}
