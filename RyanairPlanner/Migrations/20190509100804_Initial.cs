using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanairPlanner.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IataCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SeoName = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Base = table.Column<bool>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    CityCode = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirportFrom = table.Column<string>(nullable: true),
                    AirportTo = table.Column<string>(nullable: true),
                    ConnectingAirport = table.Column<string>(nullable: true),
                    NewRoute = table.Column<bool>(nullable: false),
                    SeasonalRoute = table.Column<bool>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    CarrierCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
