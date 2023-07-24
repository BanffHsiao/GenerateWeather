using System;

namespace GenerateWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            var localForecast = Weather.GetLocalWeatherForecast(Language.English);
            Console.WriteLine("Discription of today forecast: ");
            Console.WriteLine(localForecast.ForecastDesc);
        }
    }
}

