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
    }
}
