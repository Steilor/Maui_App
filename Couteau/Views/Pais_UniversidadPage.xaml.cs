using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Couteau.Views;

public partial class Pais_UniversidadPage : ContentPage
{
    public Pais_UniversidadPage()
	{
		InitializeComponent();      
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        string country = countryEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(country))
        {
            var universities = await GetUniversitiesByCountry(country);
            if (universities != null)
            {
                universityListView.ItemsSource = universities;
                universityListView.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Error", "No universities found for the specified country.", "OK");
                universityListView.IsVisible = false;
            }
        }
        else
        {
            await DisplayAlert("Input Error", "Please enter a country name.", "OK");
        }

    }
        
    private async Task<List<University>> GetUniversitiesByCountry(string country)
    {
        string url = $"http://universities.hipolabs.com/search?country={Uri.EscapeDataString(country)}";
        HttpClient client = new HttpClient();

        try
        {
            var response = await client.GetStringAsync(url);
            var universities = JsonConvert.DeserializeObject<List<University>>(response);
           
            return universities;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to retrieve data: {ex.Message}", "OK");
            return null;
        }
    }
    public class University
    {
        public string Name { get; set; }
        public List<string> Domains { get; set; }
        public List<string> WebPages { get; set; }

        public string Domain => Domains?.Count > 0 ? Domains[0] : "N/A";
        public string WebPage => WebPages?.Count > 0 ? WebPages[0] : "N/A";
    }

    

    
}
