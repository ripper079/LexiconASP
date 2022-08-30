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

        //Test 
        //Create a table
        //Table Name is People
        //Table content should be Person
        public DbSet<Person> People { get; set; }

        //This method seed the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                    new Person { 
                        IdPerson = 1,
                        FullName = "Daniel Oikarainen",
                        PhoneNumber = "033-0000000",
                        City = "Borås"
                    },
                    new Person
                    {
                        IdPerson = 2,
                        FullName = "Marko Kiwi",
                        PhoneNumber = "031-11111",
                        City = "Göteborg"
                    },
                    new Person
                    {
                        IdPerson = 3,
                        FullName = "Donald Trumph",
                        PhoneNumber = "011-222222",
                        City = "WashingtonDC"
                    },
                    new Person
                    {
                        IdPerson = 4,
                        FullName = "Vladimir Putin",
                        PhoneNumber = "007-3333333",
                        City = "Moskva"
                    }                    
                );
        }
    }
}
