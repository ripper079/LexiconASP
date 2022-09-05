﻿using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        //public int IdCountry { get; set; }

        public string CountryName { get; set; } = "";

        //Navigation Properties
        //This creates a foreign keyy....WTF magic...
        //Relationship between Countries and Cities
        public List<City> Cities { get; set; } = new List<City>();

    }
}