using System.Text.Json.Serialization;

namespace BlazorWeatherApp.Models
{
    public class OpenWeatherResponse
    {
        public List<WeatherData> List { get; set; } = new();
    }

    public class WeatherData
    {
        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        [JsonPropertyName("dt_txt")]
        public string DtTxt { get; set; } = string.Empty;

        [JsonPropertyName("main")]
        public MainData Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();

        [JsonPropertyName("wind")]
        public WindData Wind { get; set; } = new();
    }

    public class MainData
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class WindData
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }
} 