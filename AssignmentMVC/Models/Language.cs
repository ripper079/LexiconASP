using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentMVC.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        //Navigation Properties
        //This creates a foreign keyy....WTF magic...   
        /*
        //Relationships to PeopleLanguages
        public List<PersonLanguage> PeopleLanguages { get; set; }
        */


        //Relationships People
        public List<Person> People { get; set; } = new List<Person>();
    }
}
