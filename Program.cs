using System.Reflection;
using System.Resources;
using Spectre.Console;

/*
 * 
 *      Name: Weather Finder (CLI)
 *      Author: Quentin Smith
 *      Description: A terminal-based program that checks the current weather for the specified ZIP Code.
 *      Third-Party Resources Used (Project-Wide): Google Gemini (GAI), GitHub Copilot (GAI) (Built into VS2022), NOAA, and Zip Code API.
 *      Confidential Information Used: YES. The information that contains PII or involves API Keys are redacted from the final code, and a notice is put where the API key is.
 * 
 */

namespace cspWeather
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Title Text
            var title = new Rule("[cyan bold]Weather Conditions Project - AP CSP[/]\n");
            title.Justification = Justify.Center;
            AnsiConsole.Write(title);
            var zipCode = AnsiConsole.Prompt(new TextPrompt<int>("Enter a ZIP code: "));
            AnsiConsole.Clear();
            AnsiConsole.Write(title);
            var coordinates = await GetLocation.GetCoordinatesFromZip(zipCode);
            var table = new Table().Centered();
            table.ShowFooters();

            if (coordinates != null)
            {
                // The latitude and longitude used from 
                double latitude = coordinates.Item1;
                double longitude = coordinates.Item2;

                CurrentWeather weather = await GetForecast.GetCurrentWeatherAsync(latitude, longitude); // Get current weather


                // Area where the actual code (post-ZIP code and weather fetching) is.
                if (weather != null)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(title);
                    var LocationBanner = new Rule("[paleturquoise4]Weather information for " + coordinates.Item3 + ", " + coordinates.Item4 + "[/]\n");
                    LocationBanner.Justification = Justify.Center;
                    AnsiConsole.Write(LocationBanner);
                    table.Border = TableBorder.MinimalHeavyHead;
                    // Table Containing the conditions...
                    table.AddColumn("Current Conditions");
                    table.AddColumn("Extra Details");
                    table.AddRow($"[bold]{weather.ShortForecast} ({weather.DetailedForecast})[/]", $"Temp: {weather.Temperature}{weather.TemperatureUnit}\nFeels Like: {weather.FeelsLike}{weather.TemperatureUnit}\nHumidity: {weather.Humidity}%\nWind Speed: {weather.WindSpeed} mph");
                    AnsiConsole.Write(table);
                    var DateFooter = new Rule("[paleturquoise4 dim]Requested at " + TimeConvert.FormatDateTime(weather.Time) + "[/]");
                    DateFooter.Justification = Justify.Center;
                    AnsiConsole.Write(DateFooter);

                }
            }
        }
    }
}