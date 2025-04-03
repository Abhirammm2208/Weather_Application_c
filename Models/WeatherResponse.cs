using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorWeatherApp.Models
{
    public class WeatherResponse
    {
        public List<Forecast> Forecasts { get; set; } = new();
    }

    public class Forecast
    {
        public string Date { get; set; } = string.Empty;
        public TemperatureDetails TemperatureDetails { get; set; } = new();
        public List<WeatherCondition> Conditions { get; set; } = new();
        public Wind Wind { get; set; } = new();
    }

    public class TemperatureDetails
    {
        public double Temperature { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherCondition
    {
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }

    public class Wind
    {
        public double Speed { get; set; }
    }
}

