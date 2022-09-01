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
            //Naming convention I choose is that [Name]_id = foreign key
            //Contraints is applied here e.i Country_Id or City_Id Must be valid or Update-Database fails


            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1,   CountryName = "Sweden" },
                new Country { Id = 2,   CountryName = "Russia" },
                new Country { Id = 3,   CountryName = "USA" },
                new Country { Id = 4,   CountryName = "Argentina" }

                );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1,  CityName = "Borås",         Country_Id = 1 },
                new City { Id = 2,  CityName = "Gothenburg",    Country_Id = 1 },
                new City { Id = 3,  CityName = "Stockholm",     Country_Id = 1 },
                new City { Id = 4,  CityName = "Moscow",        Country_Id = 2 },
                new City { Id = 5,  CityName = "Novosibirsk",   Country_Id = 2 },
                new City { Id = 6,  CityName = "WashingtonDC",  Country_Id = 3 },
                new City { Id = 7,  CityName = "New York",      Country_Id = 3 },
                new City { Id = 8,  CityName = "Chicago",       Country_Id = 3 },
                new City { Id = 9,  CityName = "Buenos Aires",  Country_Id = 4 },
                new City { Id = 10, CityName = "Lanus",         Country_Id = 4 },
                new City { Id = 11, CityName = "Rosario",       Country_Id = 4 }
                );

            modelBuilder.Entity<Person>().HasData(
                new Person { IdPerson = 1,  FullName="Daniel Oikarainen",   PhoneNumber = "033-00000",      City_Id = 1 },
                new Person { IdPerson = 2,  FullName = "Marko Kiwi",        PhoneNumber = "033-11111",      City_Id = 2 },
                new Person { IdPerson = 3,  FullName = "Donald Trumph",     PhoneNumber = "011-222222",     City_Id = 6 },
                new Person { IdPerson = 4,  FullName = "Vladimir Putin",    PhoneNumber = "007-3333333",    City_Id = 4 },
                new Person { IdPerson = 5,  FullName = "Helge Skoog",       PhoneNumber = "033-2178328",    City_Id = 1 },
                new Person { IdPerson = 5, FullName = "Diego Maradona",     PhoneNumber = "0054-444444",    City_Id = 4 }
                );


            

        }
    }
}
