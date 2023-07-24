using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenerateWeather
{
    public class LocalWeatherForecast {
        internal LocalWeatherForecast(string json, Language language) {
            Language = language;
            if (json is null) {
                IsSucceeded = false;
                FailMessage = "Cannot instantiate LocalWeatherForecast object. JSON string is null.";
                return;
            }

            JObject jo;
            try
            {
                jo = (JObject)JsonConvert.DeserializeObject(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsSucceeded = false;
                FailMessage = "Cannot instantiate LocalWeatherForecast object. JSON deserializing failed.";
                return;
            }

            try
            {
                GeneralSituation = jo["generalSituation"].ToString();
                TCInfo = jo["tcInfo"].ToString();
                FireDangerWarning = jo["fireDangerWarning"].ToString();
                ForecastPeriod = jo["forecastPeriod"].ToString();
                ForecastDesc = jo["forecastDesc"].ToString();
                Outlook = jo["outlook"].ToString();
                UpdateTime = DateTime.Parse(jo["updateTime"].ToString());
                IsSucceeded = true;
            }
            catch (Exception e)
            {
                IsSucceeded = false;
                FailMessage =
                    $"JSON Deserializing failed. JSON string: {json}. Details:\n    {e.Source}\n    {e.Message}";
                return;
            }

            IsSucceeded = true;
        }

        public bool IsSucceeded { get; }

        public string FailMessage { get; }

        public Language Language { get; }

        public string GeneralSituation { get; }
        
        public string TCInfo { get; }
        
        public string FireDangerWarning { get; }
        
        public string ForecastPeriod { get; }
        
        public string ForecastDesc { get; }
        
        public string Outlook { get; }
        
        public DateTime UpdateTime { get; }

    }
}