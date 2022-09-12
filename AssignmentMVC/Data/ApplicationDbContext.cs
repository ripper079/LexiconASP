using AssignmentMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.Data
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        //TableName Languages with content Language
        public DbSet<Language> Languages { get; set; }

        /*
        public DbSet<PersonLanguage> PeopleLanguages { get; set; }
        */

        //This method seed the database
        //Note One limitation of seeding is that you will need to specify the primary key (i normal cases this is done by db itself)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            //When changing key to LanguageId and PersonId it works
            //These 3 statements are needed to make tha many to many connection
            /*
            modelBuilder.Entity<PersonLanguage>()
                .HasKey(pl => new { pl.LanguageId, pl.PersonId });      //Compsite key


            modelBuilder.Entity<PersonLanguage>()
                .HasOne(m => m.Person)
                .WithMany(am => am.PeopleLanguages)
                .HasForeignKey(m => m.PersonId);

            modelBuilder.Entity<PersonLanguage>()
                .HasOne(m => m.Language)
                .WithMany(am => am.PeopleLanguages)
                .HasForeignKey(m => m.LanguageId);
            */


            //Default authentication tables, do NOT manually defines the identifiers(Jonathan source)
            base.OnModelCreating(modelBuilder);

            //Make sure that database is empty, may cause problem if existing data occurs
            //Naming convention I choose is that [Name]_id = foreign key
            //Contraints is applied here e.i Country_Id or City_Id Must be valid or Update-Database fails


            //Adding Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, CountryName = "Sweden" },
                new Country { Id = 2, CountryName = "Russia" },
                new Country { Id = 3, CountryName = "USA" },
                new Country { Id = 4, CountryName = "Argentina" }

                );


            //Adding Cities
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, CityName = "Borås", Country_Id = 1 },
                new City { Id = 2, CityName = "Gothenburg", Country_Id = 1 },
                new City { Id = 3, CityName = "Stockholm", Country_Id = 1 },
                new City { Id = 4, CityName = "Moscow", Country_Id = 2 },
                new City { Id = 5, CityName = "Novosibirsk", Country_Id = 2 },
                new City { Id = 6, CityName = "WashingtonDC", Country_Id = 3 },
                new City { Id = 7, CityName = "New York", Country_Id = 3 },
                new City { Id = 8, CityName = "Chicago", Country_Id = 3 },
                new City { Id = 9, CityName = "Buenos Aires", Country_Id = 4 },
                new City { Id = 10, CityName = "Lanus", Country_Id = 4 },
                new City { Id = 11, CityName = "Rosario", Country_Id = 4 }
                );

            //Adding persons
            modelBuilder.Entity<Person>().HasData(
                new Person { IdPerson = 1, FullName = "Daniel Oikarainen", PhoneNumber = "033-00000", City_Id = 1 },
                new Person { IdPerson = 2, FullName = "Marko Kiwi", PhoneNumber = "033-11111", City_Id = 2 },
                new Person { IdPerson = 3, FullName = "Donald Trumph", PhoneNumber = "011-222222", City_Id = 6 },
                new Person { IdPerson = 4, FullName = "Vladimir Putin", PhoneNumber = "007-3333333", City_Id = 4 },
                new Person { IdPerson = 5, FullName = "Helge Skoog", PhoneNumber = "033-2178328", City_Id = 1 },
                new Person { IdPerson = 6, FullName = "Diego Maradona", PhoneNumber = "0054-444444", City_Id = 10 }
                );

            //Adding Languages
            modelBuilder.Entity<Language>().HasData(
                    new Language { Id = 1, Name = "Swedish" },
                    new Language { Id = 2, Name = "Russian" },
                    new Language { Id = 3, Name = "English" },
                    new Language { Id = 4, Name = "Hispanic" },
                    new Language { Id = 5, Name = "Polish" },
                    new Language { Id = 6, Name = "Finish" },
                    new Language { Id = 7, Name = "German" }
                );

            /*
            //Adding language to persons
            modelBuilder.Entity<PersonLanguage>().HasData(
                    new PersonLanguage { PersonId = 1, LanguageId = 1 },
                    new PersonLanguage { PersonId = 2, LanguageId = 1 },
                    new PersonLanguage { PersonId = 4, LanguageId = 2 },
                    new PersonLanguage { PersonId = 3, LanguageId = 3 }
                );
            */


            // Magic again - Creating joining table by ITSELF!!!
            // Table = LanguagePerson
            // Columnnames get PeopleIdPerson and LanguagesId

            //Daniel
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 1, LanguagesId = 1}));   //Daniel, Swedish

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 1, LanguagesId = 5 }));  //Daniel, Polish

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 1, LanguagesId = 3 }));  //Daniel, English

            //Marko
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 2, LanguagesId = 1 }));   //Marko, Swedish

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 2, LanguagesId = 6 }));  //Marko, Finish

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 2, LanguagesId = 3 }));  //Marko, English

            //Trumph
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 3, LanguagesId = 3 }));   //Trumph, English

            //Putin
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 4, LanguagesId = 2 }));   //Putin, Russia

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 4, LanguagesId = 7 }));   //Putin, German

            //Helge skog
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 5, LanguagesId = 1 }));   //Helge, Swedish

            //Maradona
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Languages)
                .WithMany(c => c.People)
                .UsingEntity(j => j.HasData(new { PeopleIdPerson = 6, LanguagesId = 4 }));   //Maradona, Hispanic



        }
    }
}
