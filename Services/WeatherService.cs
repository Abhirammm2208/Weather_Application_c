using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorWeatherApp.Models;
using Microsoft.Extensions.Configuration;

namespace BlazorWeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "a28dfc70c89af6a0e30f631e3a95e0d0";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetFiveDayForecastAsync(string city)
        {
            try
            {
                var url = $"{BaseUrl}/forecast?q={city}&appid={ApiKey}&units=metric";
                Console.WriteLine($"Requesting weather data from: {url}");

                var response = await _httpClient.GetStringAsync(url);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var weatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(response, options);

                if (weatherData?.List == null || !weatherData.List.Any())
                {
                    Console.WriteLine("Response or response.List is null or empty");
                    return new WeatherResponse { Forecasts = new List<Forecast>() };
                }

                Console.WriteLine($"Number of weather entries: {weatherData.List.Count}");

                var groupedForecasts = weatherData.List
                    .GroupBy(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).Date)
                    .Select(g => new Forecast
                    {
                        Date = g.Key.ToString("yyyy-MM-dd"),
                        TemperatureDetails = new TemperatureDetails
                        {
                            Temperature = g.Average(f => f.Main.Temp),
                            Humidity = (int)g.Average(f => f.Main.Humidity)
                        },
                        Conditions = new List<WeatherCondition>
                        {
                            new WeatherCondition
                            {
                                Description = g.First().Weather[0].Description,
                                Icon = g.First().Weather[0].Icon
                            }
                        },
                        Wind = new Wind
                        {
                            Speed = g.Average(f => f.Wind.Speed)
                        }
                    })
                    .Take(5)
                    .ToList();

                Console.WriteLine($"Processed {groupedForecasts.Count} days of forecast");
                return new WeatherResponse { Forecasts = groupedForecasts };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return new WeatherResponse { Forecasts = new List<Forecast>() };
            }
        }
    }
}
