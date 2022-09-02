using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentMVC.Migrations
{
    public partial class initseedtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Contries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Contries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.IdPerson);
                    table.ForeignKey(
                        name: "FK_People_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LanguagePerson",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "int", nullable: false),
                    PeopleIdPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagePerson", x => new { x.LanguagesId, x.PeopleIdPerson });
                    table.ForeignKey(
                        name: "FK_LanguagePerson_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagePerson_People_PeopleIdPerson",
                        column: x => x.PeopleIdPerson,
                        principalTable: "People",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contries",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { 1, "Sweden" },
                    { 2, "Russia" },
                    { 3, "USA" },
                    { 4, "Argentina" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Swedish" },
                    { 2, "Russian" },
                    { 3, "English" },
                    { 4, "Hispanic" },
                    { 5, "Polish" },
                    { 6, "Finish" },
                    { 7, "German" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName", "Country_Id" },
                values: new object[,]
                {
                    { 1, "Borås", 1 },
                    { 2, "Gothenburg", 1 },
                    { 3, "Stockholm", 1 },
                    { 4, "Moscow", 2 },
                    { 5, "Novosibirsk", 2 },
                    { 6, "WashingtonDC", 3 },
                    { 7, "New York", 3 },
                    { 8, "Chicago", 3 },
                    { 9, "Buenos Aires", 4 },
                    { 10, "Lanus", 4 },
                    { 11, "Rosario", 4 }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "IdPerson", "City_Id", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "Daniel Oikarainen", "033-00000" },
                    { 2, 2, "Marko Kiwi", "033-11111" },
                    { 3, 6, "Donald Trumph", "011-222222" },
                    { 4, 4, "Vladimir Putin", "007-3333333" },
                    { 5, 1, "Helge Skoog", "033-2178328" },
                    { 6, 4, "Diego Maradona", "0054-444444" }
                });

            migrationBuilder.InsertData(
                table: "LanguagePerson",
                columns: new[] { "LanguagesId", "PeopleIdPerson" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 5 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 6 },
                    { 5, 1 },
                    { 6, 2 },
                    { 7, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country_Id",
                table: "Cities",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePerson_PeopleIdPerson",
                table: "LanguagePerson",
                column: "PeopleIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_People_City_Id",
                table: "People",
                column: "City_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguagePerson");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Contries");
        }
    }
}
