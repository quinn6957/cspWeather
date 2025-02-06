using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System;
using System.Threading.Tasks;
using Spectre.Console;

namespace cspWeather
{
    public static class GetForecast
    {
        private static readonly HttpClient client = new();

        public static async Task<CurrentWeather> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            // THIS API KEY IS REDACTED. REPLACE WITH YOUR OWN.
            string apiKey = System.Environment.GetEnvironmentVariable("owmKey"); 
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=imperial&appid={apiKey}"; // Current weather API

            try
            {
                AnsiConsole.Markup("[gray]LOG: [/]Requesting Weather Information... ");
                var currentWeather = await client.GetFromJsonAsync<OpenWeatherMapCurrentWeather>(apiUrl);

                if (currentWeather == null)
                {
                    AnsiConsole.Markup("[red bold]FAILURE[/]\n");
                    throw new Exception("Invalid response from OpenWeatherMap API.");
                }

                var weather = new CurrentWeather
                {
                    Time = TimeConvert.ConvertFromUnixTimestamp(currentWeather.Dt), // Use the utility method
                    Temperature = (int)currentWeather.Main.Temp,
                    FeelsLike = (int)currentWeather.Main.Feels_Like,
                    TemperatureUnit = "°F",
                    ShortForecast = currentWeather.Weather[0].Main,
                    DetailedForecast = currentWeather.Weather[0].Description,
                    WindSpeed = currentWeather.Wind.Speed,
                    Humidity = currentWeather.Main.Humidity,
                };
                AnsiConsole.Markup("[green bold]SUCCESS[/]");
                return weather;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception();
            }
        }

        // Helper method
        public static int ParseInt(this string s)
        {
            if (int.TryParse(s, out int i))
            {
                return i;
            }
            return 3;
        }
    }
}