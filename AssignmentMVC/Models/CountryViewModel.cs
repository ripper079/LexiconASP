namespace AssignmentMVC.Models
{
    public class CountryViewModel
    {
        public List<Country> listOfCountries;

        public CountryViewModel()
        {
            listOfCountries = new List<Country>();
        }

        //Helper function
        public bool isIdPresent(int prospectId)
        {
            foreach (Country aCountry in listOfCountries)
            {
                if (aCountry.IdCountry == prospectId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
