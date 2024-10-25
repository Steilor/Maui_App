namespace Couteau.Views;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; // Necesitarás instalar este paquete para deserializar JSON
using Microsoft.Maui.Controls;
public partial class ClimaPage : ContentPage
{
    private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?q=Santo%20Domingo,DO&appid=150f9eadbbdbd60c557c10c84008abde&units=metric&lang=es";
    public ClimaPage()
    {
        InitializeComponent();
        GetWeatherDataAsync();
    }
    private async Task GetWeatherDataAsync()
    {
        try
        {
            // Mostrar la fecha actual
            CurrentDateLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiUrl);
                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(response);

                // Actualizar la UI con los datos de clima
                WeatherDescriptionLabel.Text = weatherData.weather[0].description;
                TemperatureLabel.Text = $"Temperatura: {weatherData.main.temp}°C";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo obtener el clima. Inténtelo de nuevo más tarde.", "OK");
            Console.WriteLine(ex.Message);
        }
    }


    // Clases para mapear la respuesta JSON
    public class WeatherResponse
    {
        public Main main { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
    }
}