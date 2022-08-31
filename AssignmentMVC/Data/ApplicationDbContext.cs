using AssignmentMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        /* Required for migration */
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Create a table(s)        
        //Tablename=People with content=Person
        public DbSet<Person> People { get; set; }
        
        //TableName 'Countries' with content 'Country'
        public DbSet<Country> Contries { get; set; }
        
        //TableName 'Cities' with content 'City'        
        public DbSet<City> Cities { get; set; }

        //This method seed the database
        //Note One limitation of seeding is that you will need to specify the primary key (i normal cases this is done by db itself)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Jonthan sa att detta behövdes
            base.OnModelCreating(modelBuilder);

            //Make sure that database is empty, may cause problem if existing data occurs

            modelBuilder.Entity<Country>().HasData(
                new Country { IdCountry = 1, CountryName = "Sweden"},
                new Country { IdCountry = 2, CountryName = "Russia" },
                new Country { IdCountry = 3, CountryName = "USA" },
                new Country { IdCountry = 4, CountryName = "Argentina" }

                );

            modelBuilder.Entity<City>().HasData(
                new City { IdCity = 1, CityName = "Borås"},
                new City { IdCity = 2, CityName = "Gothenburg" },
                new City { IdCity = 3, CityName = "Stockholm" },
                new City { IdCity = 4, CityName = "Moscow" },
                new City { IdCity = 5, CityName = "Novosibirsk" },
                new City { IdCity = 6, CityName = "WashingtonDC" },
                new City { IdCity = 7, CityName = "New York" },
                new City { IdCity = 8, CityName = "Chicago" },
                new City { IdCity = 9, CityName = "Buenos Aires" },
                new City { IdCity = 10, CityName = "Lanus" },
                new City { IdCity = 11, CityName = "Rosario" }
                );

            modelBuilder.Entity<Person>().HasData(  
                new Person { IdPerson = 1,  FullName = "Daniel Oikarainen",  PhoneNumber = "033-0000000",    City = "Borås" },
                new Person { IdPerson = 2,  FullName = "Marko Kiwi",         PhoneNumber = "031-11111",      City = "Gothenburg" },
                new Person { IdPerson = 3,  FullName = "Donald Trumph",      PhoneNumber = "011-222222",     City = "WashingtonDC" },
                new Person { IdPerson = 4,  FullName = "Vladimir Putin",     PhoneNumber = "007-3333333",    City = "Moscow" },
                new Person { IdPerson = 5,  FullName = "Helge Skoog",        PhoneNumber = "033-2178328",    City = "Borås" },
                new Person { IdPerson = 6,  FullName = "Leif Mannerström",   PhoneNumber = "031-834129",     City = "Gothenburg" },
                new Person { IdPerson = 7,  FullName = "Ronald Reagan",      PhoneNumber = "011-5781245",    City = "WashingtonDC" },
                new Person { IdPerson = 8,  FullName = "Alexander Ovechkin", PhoneNumber = "007-45891212",   City = "Moscow" },
                new Person { IdPerson = 9,  FullName = "Nikolai Karpov",     PhoneNumber = "007-57643154",   City = "Moscow" },
                new Person { IdPerson = 10, FullName = "Aleksandr Karelin", PhoneNumber = "007-98723762",   City = "Novosibirsk" },
                new Person { IdPerson = 11, FullName = "Jan-Ove",           PhoneNumber = "08-98723762",    City = "Stockholm" },
                new Person { IdPerson = 12, FullName = "Mats Sundin",       PhoneNumber = "08-91736725",    City = "Stockholm" },
                new Person { IdPerson = 13, FullName = "John Edgar Hoover", PhoneNumber = "011-9823929",    City = "WashingtonDC" },
                new Person { IdPerson = 14, FullName = "Samuel Jackson",    PhoneNumber = "011-1247182",    City = "WashingtonDC" },
                new Person { IdPerson = 15, FullName = "Michael Jordan",    PhoneNumber = "011-872435634",  City = "New York" },
                new Person { IdPerson = 16, FullName = "Al Pacino",         PhoneNumber = "011-354312447",  City = "New York" },
                new Person { IdPerson = 17, FullName = "Denzel Washington", PhoneNumber = "011-23451234",   City = "New York" },
                new Person { IdPerson = 18, FullName = "Harrison Ford",     PhoneNumber = "011-999666999",  City = "Chicago" },
                new Person { IdPerson = 19, FullName = "Hugh Hefner",       PhoneNumber = "011-69696969",   City = "Chicago" },
                new Person { IdPerson = 20, FullName = "Diego Simeone",     PhoneNumber = "0054-69696969",  City = "Buenos Aires" },
                new Person { IdPerson = 21, FullName = "Pope Francis",      PhoneNumber = "0054-69696969",  City = "Buenos Aires" },
                new Person { IdPerson = 22, FullName = "Diego Maradona",    PhoneNumber = "0054-69696969",  City = "Lanus" },
                new Person { IdPerson = 23, FullName = "Lionel Messi",      PhoneNumber = "0054-69696969",  City = "Rosario" },
                new Person { IdPerson = 24, FullName = "Che Guevara",       PhoneNumber = "0054-69696969",  City = "Rosario" }
                                                
                                                );


            //Create link between tables

        }
    }
}
