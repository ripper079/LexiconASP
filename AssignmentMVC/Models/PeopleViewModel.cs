using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class PeopleViewModel
    {

        public List<Person> listOfPersons;        
        public CreatePersonViewModel cpvm { get; set; } = new CreatePersonViewModel();

        public PeopleViewModel() 
        {
            listOfPersons = new List<Person>();
            //Populate list with 'imaginary' Persons
            PopulateWithPersons();
        }
   
        //Initialize the list
        private void PopulateWithPersons() 
        {
            //Aware that last parameter must be < 100 and here thay must be unique

            //Added refactoring city to change class city
            /*
            IdCity for Borås = 1
            IdCity for Gothenburg = 2
            IdCity for Stockholm = 3
            IdCity for Moscow = 4
            IdCity for Novosibirsk = 5
            IdCity for WashingtonDC = 6
            IdCity for New York = 7
            IdCity for Chicago = 8
            IdCity for Buenos Aires = 9
            IdCity for Lanus = 10
            IdCity for Rosario = 11
            */

            addPersonToList("Daniel Oikarainen",    "033-0000000",      "Borås",            1,      1);
            addPersonToList("Marko Kiwi",           "031-11111",        "Gothenburg",       2,      2);
            addPersonToList("Donald Trumph",        "011-222222",       "WashingtonDC",     3,      6);
            addPersonToList("Vladimir Putin",       "007-3333333",      "Moscow",           4,      4);
            addPersonToList("Santa Kall",           "123-12456",        "Stockholm",        5,      3);
            addPersonToList("Kalle Svensson",       "033-3421",         "Stockholm",        6,      3);
            addPersonToList("Diego Maradona",       "0054-444444",      "Lanus",            7,      10);
            addPersonToList("Lionel Messi",         "5554-444444",      "Rosario",          8,      11);            
        }
        
        //When none idForCity is passes (backward compatibility) it will give a dummy value of 666. Its safe because prior program didn't utilize idForCity
        public void addPersonToList(string fullName, string phoneNumber, string city, int idForPerson, int idForCity=666)
        {

            listOfPersons.Add(
                new Person() 
                {
                    IdPerson = idForPerson,
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    CityOfPerson = new City { CityName = city , Id = idForCity }
                }
            );
        }
        
        //Helper function
        public bool isIdPresent(int prospectId) 
        {
            foreach (Person person in listOfPersons) 
            {
                if (person.IdPerson == prospectId)
                {
                    return true;
                }
            }
            return false;
        }

        public void removePersonFromList(int validId) 
        {
            foreach(Person person in listOfPersons)
            {
                if (person.IdPerson == validId) 
                {
                    listOfPersons.Remove(person);
                    break;
                }
            }            
        }

        //Filters the original list(viewmodel). VERY vague description in assigment paper. Colleagues has 3 different Interpretations of "Search"
        public void filterList(string filterByFullName, string filterByPhoneNumber, string filterByCity) 
        {
            List<Person> resultingList = listOfPersons;            

            if (! String.IsNullOrEmpty(filterByFullName))
            {
                resultingList = resultingList.Where(item => String.Equals(item.FullName, filterByFullName)).ToList();
            }

            if (! String.IsNullOrEmpty(filterByPhoneNumber))
            {
                resultingList = resultingList.Where(item => String.Equals(item.PhoneNumber, filterByPhoneNumber)).ToList();
            }
            
            if (! String.IsNullOrEmpty(filterByCity))
            {
                resultingList = resultingList.Where(item => String.Equals(item.CityOfPerson.CityName, filterByCity)).ToList();
            }

            //New resulting filter
            listOfPersons = resultingList;
        }
       
        
        
    }
}
