namespace AssignmentMVC.Models
{
    public class LanguageViewModel
    {
        public List<Language> listOfLanguages;

        public LanguageViewModel()
        {
            listOfLanguages = new List<Language>();
        }

        //Helper function
        public bool isIdPresent(int prospectId)
        {
            foreach (Language aLanguagey in listOfLanguages)
            {
                if (aLanguagey.Id == prospectId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
