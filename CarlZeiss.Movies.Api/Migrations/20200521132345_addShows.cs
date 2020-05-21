using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarlZeiss.Movies.Api.Migrations
{
    public partial class addShows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieId = table.Column<int>(nullable: false),
                    MultiplexId = table.Column<int>(nullable: false),
                    ShowDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Multiplexes_MultiplexId",
                        column: x => x.MultiplexId,
                        principalTable: "Multiplexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MultiplexId",
                table: "Shows",
                column: "MultiplexId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieId_MultiplexId_ShowDate",
                table: "Shows",
                columns: new[] { "MovieId", "MultiplexId", "ShowDate" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
