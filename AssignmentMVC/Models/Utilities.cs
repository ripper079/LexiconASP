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
    }
}
