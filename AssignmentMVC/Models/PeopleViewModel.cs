using System.Collections.Generic;

namespace AssignmentMVC.Models
{
    public class PeopleViewModel
    {
        public List<Person> listOfPersons;
        //public string FullName { get; set; }
        //public string City { get; set; }
        //public string PhoneNumber { get; set; }
        /*public Person person = new Person("foo", "foo2", "foo3");*/

        public PeopleViewModel() 
        {
            //Populate with experminatal data
            listOfPersons = new List<Person>();

            PopulateWithPersons();
        }

        private void PopulateWithPersons() 
        {               
            addPersonToList("Daniel Oikarainen", "033-0000000", "Borås");
            addPersonToList("Marko Kiwi", "031-11111", "Göteborg");
            addPersonToList("Donald Trumph", "011-222222", "WashingtonDC");
            addPersonToList("Vladimir Putin", "007-3333333", "Moskva");
            addPersonToList("Santa Kall", "123-12456", "Kiruna");
            addPersonToList("Kalle Svensson", "033-3421", "Stockholm");
            addPersonToList("Diego Maradona", "0054-444444", "Buenos Aires");
            addPersonToList("Lionel Messi", "5554-444444", "Buenos Aires");
            addPersonToList("Kalle Orvarsson", "132-1234", "Korvstaden");
            addPersonToList("Olle Svensson", "222-121844", "Kiruna");
            addPersonToList("Hanna Eriksson", "033-111111", "Stockholm");
            addPersonToList("Kalle Svensson", "033-222222", "Borås");
            addPersonToList("Lejon Simba", "666-666-666", "Savanen");
            addPersonToList("Glenn Hysen", "123-789-111", "Göteborg");
            addPersonToList("Valrossen Ross", "0054-444444", "Bubbel havet");

        }

        public void addPersonToList(string fullName, string phoneNumber, string city)
        {
            listOfPersons.Add(
                new Person() 
                {
                    IdPerson = Person.GenerateIdNumberForPerson(),
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    City = city
                }
            );
        }

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

        public void filterList(string filterByFullName, string filterByPhoneNumber, string filterByCity) 
        {
            List<Person> resultingList = listOfPersons;

            ////Filter away name
            //List<Person> myFilteredAwayName = listOfPersons.Where(item => String.Equals(item.FullName, filterByFullName)).ToList();
            ////Filter away phone number
            //List<Person> myFilteredAwayPhoneNumber = myFilteredAwayName.Where(item => String.Equals(item.PhoneNumber, filterByPhoneNumber)).ToList();
            ////Filter away city
            //List<Person> myFilteredAwayCity = myFilteredAwayPhoneNumber.Where(item => String.Equals(item.City, filterByCity)).ToList();

            if (! String.IsNullOrEmpty(filterByFullName))
            {
                resultingList = resultingList.Where(item => String.Equals(item.FullName, filterByFullName)).ToList();
            }

            //if (filterByPhoneNumber != null) 
            if (! String.IsNullOrEmpty(filterByPhoneNumber))
            {
                resultingList = resultingList.Where(item => String.Equals(item.PhoneNumber, filterByPhoneNumber)).ToList();
            }
            
            //if (filterByCity != null) 
            if (! String.IsNullOrEmpty(filterByCity))
            {
                resultingList = resultingList.Where(item => String.Equals(item.City, filterByCity)).ToList();
            }


            //New resulting filter
            listOfPersons = resultingList;
        }

        public List<Person> GetAllPersons() 
        {
            return listOfPersons;
        }

        //Make sure that the lis is not empty
        public Person GetFirstPersonInList() 
        {
            return listOfPersons[0];
        }

        public Person GetLastPersonInList()
        {
            return listOfPersons[listOfPersons.Count - 1];
        }

        public int GetNumberOfPersonInList() 
        {
            return listOfPersons.Count;
        }
    }
}
