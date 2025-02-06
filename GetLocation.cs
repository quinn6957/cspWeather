using System.Net.Http.Json;
using System.Text.Json;
using Spectre.Console;

/*
 * 
 *      Name: ZIP Code to Lat/Long Degrees.
 *      Author: Quentin Smith
 *      Description: Converts a provided ZIP code to latitude and longitude degrees.
 *      Third-Party Resources Used: ZIP Code API.
 * 
 */

namespace cspWeather
{
    public class GetLocation
    {
        private static readonly HttpClient client = new();

        public static async Task<Tuple<double, double, string, string>> GetCoordinatesFromZip(int zipCode)
        {
            // API to get latitude and longitude data from a ZIP code.
            string geocodingUrl = $"https://www.zipcodeapi.com/rest/CeLl2c8oMYhTE4k1Aof1BJYWsABIOFx6glOegARGOYjbU4evvBb41eGI6eT2bpFB/info.json/{zipCode}/degrees";
            try
            {
                var response = await client.GetAsync(geocodingUrl);
                AnsiConsole.Markup($"[gray]LOG: [/]Getting coordinate information for \"{zipCode}\"... ");
                response.EnsureSuccessStatusCode(); // Check for successful response                

                var geocodingData = await response.Content.ReadFromJsonAsync<ZipcodeApiResponse>();

                if (geocodingData != null)
                {
                    AnsiConsole.Markup("[green bold]SUCCESS[/]\n");
                    return Tuple.Create(geocodingData.Lat, geocodingData.Lng, geocodingData.City, geocodingData.State);
                }
                else
                {
                    AnsiConsole.Markup("[red bold]FAILURE[/]\n");

                    AnsiConsole.MarkupLine($"[bold red]Could not find coordinates for ZIP code: {zipCode}[/]");
                    throw new ApplicationException();
                }
            }
            catch (HttpRequestException ex)
            {
                AnsiConsole.Markup("[red bold]FAILURE[/]\n");
                AnsiConsole.MarkupLine($"[bold red]Geocoding API Error: {ex.Message}[/]");
                throw new HttpRequestException();
            }
            catch (JsonException ex)
            {
                AnsiConsole.Markup("[red bold]FAILURE[/]\n");
                AnsiConsole.MarkupLine($"[bold red]Error parsing JSON response: {ex.Message}[/]");
                throw new JsonException();
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup("[red bold]FAILURE[/]\n");
                AnsiConsole.MarkupLine($"[bold red]An error occurred: {ex.Message}[/]");
                throw new Exception();
            }
        }

    }
}
