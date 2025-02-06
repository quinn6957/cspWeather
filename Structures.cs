/*
 *
 *      Name: Project Structures.
 *      Author: Quentin Smith.
 *      Description: All the data structures in this project. Contains the data structures for the current weather, and the latitude and longitude structures.
 *      Third-Party Resources Used: None used.
 * 
 */
using System.Text.Json.Serialization;

namespace cspWeather
{
    public class WeatherForecast
    {
        public required List<CurrentWeather> Forecasts { get; set; }
    }
    public class CurrentWeather
    {
        public DateTime Time { get; set; }
        public int Temperature { get; set; }
        public int FeelsLike { get; set; }
        public required string TemperatureUnit { get; set; }
        public required string ShortForecast { get; set; }
        public required string DetailedForecast { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }

    }

    public class OpenWeatherMapCurrentWeather
    {
        public long Dt { get; set; }
        public required MainInfo Main { get; set; }
        public required List<WeatherInfo> Weather { get; set; }
        public required WindInfo Wind { get; set; } 
        public int Humidity { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        public required string Main { get; set; }
        public required string Description { get; set; }

    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }

    // API Response Structure.
    public class ZipcodeApiResponse
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [JsonPropertyName("state")]
        public required string State { get; set; }

        [JsonPropertyName("city")]
        public required string City { get; set; }
    }
}