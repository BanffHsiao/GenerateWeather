using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GenerateWeather {
    public static class Weather
    {
        public static LocalWeatherForecast GetLocalWeatherForecast(Language language = Language.English)
        {
            var json = HttpRequest(GenerateRequestUrl(language, WeatherDataType.LocalWeatherForecast));
            return string.IsNullOrEmpty(json)
                ? null
                : JsonIsValid(json)
                    ? new LocalWeatherForecast(json, language)
                    : null;
        }

        private static string GenerateRequestUrl(Language language, WeatherDataType dataType)
        {
            var url = "https://data.weather.gov.hk/weatherAPI/opendata/weather.php?";

            switch (language)
            {
                case Language.English:
                    url += "lang=en";
                    break;
                case Language.TraditionalChinese:
                    url += "lang=tc";
                    break;
                case Language.SimplifiedChinese:
                    url += "lang=sc";
                    break;
            };

            switch (dataType)
            {
                case WeatherDataType.LocalWeatherForecast:
                    url += "&dataType=flw";
                    break;
                case WeatherDataType.NineDaysWeather:
                    url += "&dataType=fnd";
                    break;
            }

            return url;
        }

        private static string HttpRequest(string url)
        {
            string response;
            try
            {
                if (UT_HttpRequestFail)
                    throw new Exception("This was thrown by Unit Test");

                var request = WebRequest.Create(url);
                request.Method = "GET";
                using var responseStream = request.GetResponse().GetResponseStream();
                using var reader = new StreamReader(responseStream);
                response = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return response;
        }

        private static bool JsonIsValid(string json)
        {
            try
            {
                // Try parsing invalid json if specified
                JObject.Parse(UT_InvalidJson ?? json);
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static bool UT_HttpRequestFail { get; set; }
        internal static string UT_InvalidJson { get; set; }
    }
}