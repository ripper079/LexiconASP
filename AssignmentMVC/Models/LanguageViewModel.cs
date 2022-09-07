using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class LanguageViewModel
    {
        //public List<Language> listOfLanguages = new List<Language>();

        
        ////public LanguageViewModel()
        ////{
        ////    listOfLanguages = new List<Language>();
        ////}

        ////Helper function
        //public bool isIdPresent(int prospectId)
        //{
        //    foreach (Language aLanguagey in listOfLanguages)
        //    {
        //        if (aLanguagey.Id == prospectId)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
    }
}
