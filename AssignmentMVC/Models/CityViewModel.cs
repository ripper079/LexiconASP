using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.Models
{
    public class CityViewModel
    {


        //public List<City> listOfCities;

        //public CityViewModel()
        //{
        //    listOfCities = new List<City>();
        //}

        ////Helper function
        //public bool isIdPresent(int prospectId)
        //{
        //    foreach (City aCity in listOfCities)
        //    {
        //        //if (aCity.IdCity == prospectId)
        //        if (aCity.Id == prospectId)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public int Id { get; set; }
        
        [Required]
        public string CityName { get; set; }

        public int Country_Id { get; set; }

    }
}
