using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentMVC.Migrations
{
    public partial class SeedPeopleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "IdPerson", "City", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Borås", "Daniel Oikarainen", "033-0000000" },
                    { 2, "Göteborg", "Marko Kiwi", "031-11111" },
                    { 3, "WashingtonDC", "Donald Trumph", "011-222222" },
                    { 4, "Moskva", "Vladimir Putin", "007-3333333" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "IdPerson",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "IdPerson",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "IdPerson",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "IdPerson",
                keyValue: 4);
        }
    }
}
