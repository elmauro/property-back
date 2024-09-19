using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MC.PropertyService.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CodeInternal = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    PropertyImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    File = table.Column<string>(type: "text", nullable: false),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.PropertyImageId);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTraces",
                columns: table => new
                {
                    PropertyTraceId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateSale = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTraces", x => x.PropertyTraceId);
                    table.ForeignKey(
                        name: "FK_PropertyTraces_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "Address", "Birthday", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "Photo" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "123 Main St, Springfield", new DateTime(1980, 5, 20, 5, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(231), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(234), new TimeSpan(0, 0, 0, 0, 0)), "", "John Doe", "johndoe.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "456 Oak Ave, Springfield", new DateTime(1985, 3, 15, 5, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(242), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(243), new TimeSpan(0, 0, 0, 0, 0)), "", "Jane Smith", "janesmith.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "789 Pine Rd, Springfield", new DateTime(1990, 11, 10, 5, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(248), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 448, DateTimeKind.Unspecified).AddTicks(248), new TimeSpan(0, 0, 0, 0, 0)), "", "Robert Brown", "robertbrown.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Address", "CodeInternal", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "OwnerId", "Price", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), "123 Country Road, Springfield", "GR12345", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(677), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(679), new TimeSpan(0, 0, 0, 0, 0)), "", "Green Acres", new Guid("00000000-0000-0000-0000-000000000001"), 250000m, 2010 },
                    { new Guid("00000000-0000-0000-0000-000000000102"), "456 Beach Blvd, Springfield", "OV45678", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(685), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(686), new TimeSpan(0, 0, 0, 0, 0)), "", "Ocean View", new Guid("00000000-0000-0000-0000-000000000002"), 450000m, 2015 },
                    { new Guid("00000000-0000-0000-0000-000000000103"), "789 Hilltop Dr, Springfield", "MR78910", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)), "", "Mountain Retreat", new Guid("00000000-0000-0000-0000-000000000003"), 350000m, 2012 },
                    { new Guid("00000000-0000-0000-0000-000000000104"), "101 Downtown Ave, Springfield", "CL10111", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(695), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(695), new TimeSpan(0, 0, 0, 0, 0)), "", "City Lights", new Guid("00000000-0000-0000-0000-000000000001"), 600000m, 2020 },
                    { new Guid("00000000-0000-0000-0000-000000000105"), "202 Suburb Ln, Springfield", "SD20222", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(700), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(701), new TimeSpan(0, 0, 0, 0, 0)), "", "Suburban Dream", new Guid("00000000-0000-0000-0000-000000000002"), 300000m, 2005 },
                    { new Guid("00000000-0000-0000-0000-000000000106"), "303 City Center Rd, Springfield", "DL30333", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(716), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(717), new TimeSpan(0, 0, 0, 0, 0)), "", "Downtown Loft", new Guid("00000000-0000-0000-0000-000000000003"), 450000m, 2018 },
                    { new Guid("00000000-0000-0000-0000-000000000107"), "404 Rural Dr, Springfield", "CE40444", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(721), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(721), new TimeSpan(0, 0, 0, 0, 0)), "", "Countryside Estate", new Guid("00000000-0000-0000-0000-000000000001"), 700000m, 2017 },
                    { new Guid("00000000-0000-0000-0000-000000000108"), "505 Lakeside Rd, Springfield", "LH50555", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(727), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(728), new TimeSpan(0, 0, 0, 0, 0)), "", "Lakehouse", new Guid("00000000-0000-0000-0000-000000000002"), 800000m, 2019 },
                    { new Guid("00000000-0000-0000-0000-000000000109"), "606 High Rise Blvd, Springfield", "PH60666", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(734), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(736), new TimeSpan(0, 0, 0, 0, 0)), "", "Penthouse Suite", new Guid("00000000-0000-0000-0000-000000000003"), 950000m, 2021 },
                    { new Guid("00000000-0000-0000-0000-000000000110"), "707 Farm Ln, Springfield", "CC70777", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(740), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(740), new TimeSpan(0, 0, 0, 0, 0)), "", "Country Cottage", new Guid("00000000-0000-0000-0000-000000000001"), 200000m, 2000 },
                    { new Guid("00000000-0000-0000-0000-000000000111"), "808 Palm Ave, Springfield", "LV80888", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(744), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(744), new TimeSpan(0, 0, 0, 0, 0)), "", "Luxury Villa", new Guid("00000000-0000-0000-0000-000000000002"), 1200000m, 2022 },
                    { new Guid("00000000-0000-0000-0000-000000000112"), "909 River Rd, Springfield", "BB90999", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(748), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(749), new TimeSpan(0, 0, 0, 0, 0)), "", "Bungalow Bliss", new Guid("00000000-0000-0000-0000-000000000003"), 275000m, 2008 },
                    { new Guid("00000000-0000-0000-0000-000000000113"), "1001 Woods Ln, Springfield", "FL101010", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(752), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(753), new TimeSpan(0, 0, 0, 0, 0)), "", "Forest Lodge", new Guid("00000000-0000-0000-0000-000000000001"), 650000m, 2016 },
                    { new Guid("00000000-0000-0000-0000-000000000114"), "1102 Coastal Rd, Springfield", "SC111011", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(762), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(762), new TimeSpan(0, 0, 0, 0, 0)), "", "Seaside Cottage", new Guid("00000000-0000-0000-0000-000000000002"), 300000m, 2011 },
                    { new Guid("00000000-0000-0000-0000-000000000115"), "1203 Metro Blvd, Springfield", "US121212", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(766), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(767), new TimeSpan(0, 0, 0, 0, 0)), "", "Urban Studio", new Guid("00000000-0000-0000-0000-000000000003"), 350000m, 2014 },
                    { new Guid("00000000-0000-0000-0000-000000000116"), "1304 Valley Rd, Springfield", "HM131313", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 0, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(810), new TimeSpan(0, 0, 0, 0, 0)), "", "Hillside Manor", new Guid("00000000-0000-0000-0000-000000000001"), 500000m, 2023 }
                });

            migrationBuilder.InsertData(
                table: "PropertyImages",
                columns: new[] { "PropertyImageId", "CreatedAt", "CreatedBy", "Enabled", "File", "LastUpdatedAt", "LastUpdatedBy", "PropertyId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6692), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "green_acres_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6693), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000101") },
                    { new Guid("00000000-0000-0000-0000-000000000102"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6698), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "ocean_view_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6700), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000102") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6703), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "mountain_retreat_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6704), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000103") },
                    { new Guid("00000000-0000-0000-0000-000000000104"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6707), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "city_lights_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(6707), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000104") },
                    { new Guid("00000000-0000-0000-0000-000000000105"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7313), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "suburban_dream_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7314), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000105") },
                    { new Guid("00000000-0000-0000-0000-000000000106"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7326), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "downtown_loft_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7327), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000106") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7330), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "countryside_estate_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7331), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000107") },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7333), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "lakehouse_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7334), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000108") },
                    { new Guid("00000000-0000-0000-0000-000000000109"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7337), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "penthouse_suite_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7338), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000109") },
                    { new Guid("00000000-0000-0000-0000-000000000110"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7341), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "country_cottage_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7341), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000110") },
                    { new Guid("00000000-0000-0000-0000-000000000111"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7344), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "luxury_villa_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7345), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000111") },
                    { new Guid("00000000-0000-0000-0000-000000000112"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7348), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "bungalow_bliss_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7348), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000112") },
                    { new Guid("00000000-0000-0000-0000-000000000113"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7351), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "forest_lodge_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7352), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000113") },
                    { new Guid("00000000-0000-0000-0000-000000000114"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7358), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "seaside_cottage_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7358), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000114") },
                    { new Guid("00000000-0000-0000-0000-000000000115"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7361), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "urban_studio_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7362), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000115") },
                    { new Guid("00000000-0000-0000-0000-000000000116"), new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7364), new TimeSpan(0, 0, 0, 0, 0)), "", 1, "hillside_manor_1.jpg", new DateTimeOffset(new DateTime(2024, 9, 18, 4, 8, 34, 449, DateTimeKind.Unspecified).AddTicks(7365), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("00000000-0000-0000-0000-000000000116") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CreatedAt",
                table: "Owners",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CreatedAt",
                table: "Properties",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_CreatedAt",
                table: "PropertyImages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTraces_CreatedAt",
                table: "PropertyTraces",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTraces_PropertyId",
                table: "PropertyTraces",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "PropertyTraces");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
