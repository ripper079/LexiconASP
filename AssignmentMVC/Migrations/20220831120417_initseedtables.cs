using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentMVC.Migrations
{
    public partial class initseedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                });

            migrationBuilder.CreateTable(
                name: "Contries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contries", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.IdPerson);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "IdCity", "CityName" },
                values: new object[,]
                {
                    { 1, "Borås" },
                    { 2, "Gothenburg" },
                    { 3, "Stockholm" },
                    { 4, "Moscow" },
                    { 5, "Novosibirsk" },
                    { 6, "WashingtonDC" },
                    { 7, "New York" },
                    { 8, "Chicago" },
                    { 9, "Buenos Aires" },
                    { 10, "Lanus" },
                    { 11, "Rosario" }
                });

            migrationBuilder.InsertData(
                table: "Contries",
                columns: new[] { "IdCountry", "CountryName" },
                values: new object[,]
                {
                    { 1, "Sweden" },
                    { 2, "Russia" },
                    { 3, "USA" },
                    { 4, "Argentina" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "IdPerson", "City", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Borås", "Daniel Oikarainen", "033-0000000" },
                    { 2, "Gothenburg", "Marko Kiwi", "031-11111" },
                    { 3, "WashingtonDC", "Donald Trumph", "011-222222" },
                    { 4, "Moscow", "Vladimir Putin", "007-3333333" },
                    { 5, "Borås", "Helge Skoog", "033-2178328" },
                    { 6, "Gothenburg", "Leif Mannerström", "031-834129" },
                    { 7, "WashingtonDC", "Ronald Reagan", "011-5781245" },
                    { 8, "Moscow", "Alexander Ovechkin", "007-45891212" },
                    { 9, "Moscow", "Nikolai Karpov", "007-57643154" },
                    { 10, "Novosibirsk", "Aleksandr Karelin", "007-98723762" },
                    { 11, "Stockholm", "Jan-Ove", "08-98723762" },
                    { 12, "Stockholm", "Mats Sundin", "08-91736725" },
                    { 13, "WashingtonDC", "John Edgar Hoover", "011-9823929" },
                    { 14, "WashingtonDC", "Samuel Jackson", "011-1247182" },
                    { 15, "New York", "Michael Jordan", "011-872435634" },
                    { 16, "New York", "Al Pacino", "011-354312447" },
                    { 17, "New York", "Denzel Washington", "011-23451234" },
                    { 18, "Chicago", "Harrison Ford", "011-999666999" },
                    { 19, "Chicago", "Hugh Hefner", "011-69696969" },
                    { 20, "Buenos Aires", "Diego Simeone", "0054-69696969" },
                    { 21, "Buenos Aires", "Pope Francis", "0054-69696969" },
                    { 22, "Lanus", "Diego Maradona", "0054-69696969" },
                    { 23, "Rosario", "Lionel Messi", "0054-69696969" },
                    { 24, "Rosario", "Che Guevara", "0054-69696969" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Contries");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
