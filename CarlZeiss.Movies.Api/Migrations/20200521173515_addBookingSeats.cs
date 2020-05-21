using Microsoft.EntityFrameworkCore.Migrations;

namespace CarlZeiss.Movies.Api.Migrations
{
    public partial class addBookingSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.CreateTable(
                name: "MasterSeats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNo = table.Column<int>(nullable: false),
                    MultiplexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterSeats_Multiplexes_MultiplexId",
                        column: x => x.MultiplexId,
                        principalTable: "Multiplexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookedSeats",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    SeatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedSeats", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookedSeats_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookedSeats_MasterSeats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "MasterSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_SeatId",
                table: "BookedSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterSeats_MultiplexId",
                table: "MasterSeats",
                column: "MultiplexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedSeats");

            migrationBuilder.DropTable(
                name: "MasterSeats");

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    SeatNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => new { x.BookingId, x.SeatNo });
                    table.ForeignKey(
                        name: "FK_Seat_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
