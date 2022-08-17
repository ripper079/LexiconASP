namespace AssignmentMVC.Models
{
    public class Utilities
    {     
        public static string GetTemperatureStatusInCelsius(int? temperature)
        {
            string healthStatusTemp = "";

            if (temperature <= 36)
            {
                healthStatusTemp = temperature + " Celcius is " + "Hypothermia";
            }

            else if (temperature >= 38)
            {
                healthStatusTemp = temperature + " Celcius is " + "Fever";
            }

            else
            {
                healthStatusTemp = temperature + " Celcius is " + "Normal";
            }

            return healthStatusTemp;
        }

        //Generate a number between 1 and 100
        public static int GenerateSecretNumber() 
        {
            return new Random().Next(1, 101);
        }
    }
}
