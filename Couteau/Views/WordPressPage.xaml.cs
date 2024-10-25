using System.Collections.ObjectModel;
using static System.Collections.Specialized.NameObjectCollectionBase;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace Couteau.Views;

public partial class WordPressPage : ContentPage
{
    private const string WordPressApiUrl = "https://yourwordpresssite.com/wp-json/wp/v2/posts?per_page=3";
    private const string SiteLogoUrl = "https://s.w.org/style/images/about/WordPress-logotype-standard.png"; // Reemplaza con la URL del logo
    public ObservableCollection<NewsItem> NewsItems { get; set; }
    public WordPressPage()
	{
		InitializeComponent();
        NewsItems = new ObservableCollection<NewsItem>();
        NewsCollection.ItemsSource = NewsItems;
        LoadSiteLogo();
        GetLatestNewsAsync();
    }
    private async void LoadSiteLogo()
    {
        // Cargar el logo del sitio web
        SiteLogo.Source = ImageSource.FromUri(new Uri(SiteLogoUrl));
    }

    private async Task GetLatestNewsAsync()
    {
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(WordPressApiUrl);
                var news = JsonConvert.DeserializeObject<NewsItem[]>(response);

                // Agregar las noticias a la lista
                foreach (var item in news)
                {
                    item.OpenNewsCommand = new Command(() => OpenNewsInBrowser(item.link));
                    NewsItems.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron obtener las noticias. Inténtelo de nuevo más tarde.", "OK");
            Console.WriteLine(ex.Message);
        }
    }

    private async void OpenNewsInBrowser(string url)
    {
        try
        {
            await Launcher.Default.OpenAsync(new Uri(url));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo abrir la noticia en el navegador.", "OK");
            Console.WriteLine(ex.Message);
        }
    }
    // Clases para mapear la respuesta JSON
    public class NewsItem
    {
        public string link { get; set; }
        public Title title { get; set; }
        public Excerpt excerpt { get; set; }
        public Command OpenNewsCommand { get; set; }
    }

    public class Title
    {
        public string rendered { get; set; }
    }

    public class Excerpt
    {
        public string rendered { get; set; }
    }
}