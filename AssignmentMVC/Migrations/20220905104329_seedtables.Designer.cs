﻿// <auto-generated />
using AssignmentMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssignmentMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220905104329_seedtables")]
    partial class seedtables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AssignmentMVC.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Country_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Country_Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "Borås",
                            Country_Id = 1
                        },
                        new
                        {
                            Id = 2,
                            CityName = "Gothenburg",
                            Country_Id = 1
                        },
                        new
                        {
                            Id = 3,
                            CityName = "Stockholm",
                            Country_Id = 1
                        },
                        new
                        {
                            Id = 4,
                            CityName = "Moscow",
                            Country_Id = 2
                        },
                        new
                        {
                            Id = 5,
                            CityName = "Novosibirsk",
                            Country_Id = 2
                        },
                        new
                        {
                            Id = 6,
                            CityName = "WashingtonDC",
                            Country_Id = 3
                        },
                        new
                        {
                            Id = 7,
                            CityName = "New York",
                            Country_Id = 3
                        },
                        new
                        {
                            Id = 8,
                            CityName = "Chicago",
                            Country_Id = 3
                        },
                        new
                        {
                            Id = 9,
                            CityName = "Buenos Aires",
                            Country_Id = 4
                        },
                        new
                        {
                            Id = 10,
                            CityName = "Lanus",
                            Country_Id = 4
                        },
                        new
                        {
                            Id = 11,
                            CityName = "Rosario",
                            Country_Id = 4
                        });
                });

            modelBuilder.Entity("AssignmentMVC.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryName = "Sweden"
                        },
                        new
                        {
                            Id = 2,
                            CountryName = "Russia"
                        },
                        new
                        {
                            Id = 3,
                            CountryName = "USA"
                        },
                        new
                        {
                            Id = 4,
                            CountryName = "Argentina"
                        });
                });

            modelBuilder.Entity("AssignmentMVC.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Swedish"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Russian"
                        },
                        new
                        {
                            Id = 3,
                            Name = "English"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Hispanic"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Polish"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Finish"
                        },
                        new
                        {
                            Id = 7,
                            Name = "German"
                        });
                });

            modelBuilder.Entity("AssignmentMVC.Models.Person", b =>
                {
                    b.Property<int>("IdPerson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPerson"), 1L, 1);

                    b.Property<int>("City_Id")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPerson");

                    b.HasIndex("City_Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            IdPerson = 1,
                            City_Id = 1,
                            FullName = "Daniel Oikarainen",
                            PhoneNumber = "033-00000"
                        },
                        new
                        {
                            IdPerson = 2,
                            City_Id = 2,
                            FullName = "Marko Kiwi",
                            PhoneNumber = "033-11111"
                        },
                        new
                        {
                            IdPerson = 3,
                            City_Id = 6,
                            FullName = "Donald Trumph",
                            PhoneNumber = "011-222222"
                        },
                        new
                        {
                            IdPerson = 4,
                            City_Id = 4,
                            FullName = "Vladimir Putin",
                            PhoneNumber = "007-3333333"
                        },
                        new
                        {
                            IdPerson = 5,
                            City_Id = 1,
                            FullName = "Helge Skoog",
                            PhoneNumber = "033-2178328"
                        },
                        new
                        {
                            IdPerson = 6,
                            City_Id = 10,
                            FullName = "Diego Maradona",
                            PhoneNumber = "0054-444444"
                        });
                });

            modelBuilder.Entity("LanguagePerson", b =>
                {
                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.Property<int>("PeopleIdPerson")
                        .HasColumnType("int");

                    b.HasKey("LanguagesId", "PeopleIdPerson");

                    b.HasIndex("PeopleIdPerson");

                    b.ToTable("LanguagePerson");

                    b.HasData(
                        new
                        {
                            LanguagesId = 1,
                            PeopleIdPerson = 1
                        },
                        new
                        {
                            LanguagesId = 5,
                            PeopleIdPerson = 1
                        },
                        new
                        {
                            LanguagesId = 3,
                            PeopleIdPerson = 1
                        },
                        new
                        {
                            LanguagesId = 1,
                            PeopleIdPerson = 2
                        },
                        new
                        {
                            LanguagesId = 6,
                            PeopleIdPerson = 2
                        },
                        new
                        {
                            LanguagesId = 3,
                            PeopleIdPerson = 2
                        },
                        new
                        {
                            LanguagesId = 3,
                            PeopleIdPerson = 3
                        },
                        new
                        {
                            LanguagesId = 2,
                            PeopleIdPerson = 4
                        },
                        new
                        {
                            LanguagesId = 7,
                            PeopleIdPerson = 4
                        },
                        new
                        {
                            LanguagesId = 1,
                            PeopleIdPerson = 5
                        },
                        new
                        {
                            LanguagesId = 4,
                            PeopleIdPerson = 6
                        });
                });

            modelBuilder.Entity("AssignmentMVC.Models.City", b =>
                {
                    b.HasOne("AssignmentMVC.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("Country_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AssignmentMVC.Models.Person", b =>
                {
                    b.HasOne("AssignmentMVC.Models.City", "CityOfPerson")
                        .WithMany("People")
                        .HasForeignKey("City_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CityOfPerson");
                });

            modelBuilder.Entity("LanguagePerson", b =>
                {
                    b.HasOne("AssignmentMVC.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssignmentMVC.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PeopleIdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssignmentMVC.Models.City", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("AssignmentMVC.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
