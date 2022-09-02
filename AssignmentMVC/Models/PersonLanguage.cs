using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentMVC.Models
{

    //This act as a joining table
    //Ehh look like not need
    //Entity framework work auto-magic...
    public class PersonLanguage
    {
        //[Key]
        //public int Id { get; set; }

        
        
        //Relationship to People
        //public int Person_Id { get; set; }
        //[ForeignKey("Person_Id")]
        
        /*
        public int PersonId { get; set; }
        public Person Person;
        */


        //Relationships to Languages
        //public int Language_Id { get; set; }
        //[ForeignKey("Language_Id")]

        /*
        public int LanguageId { get; set; }
        public Language Language;
        */
    }
}
