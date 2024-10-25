namespace Couteau.Views;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

public partial class Nombre_GeneroPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    public Nombre_GeneroPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Error", "Por favor ingrese un nombre.", "OK");
            return;
        }

        // Llamada a la API 
        string url = $"https://api.genderize.io/?name={name}";
        var response = await _httpClient.GetStringAsync(url);

        // Deserialización de la respuesta
        var genderPrediction = JsonConvert.DeserializeObject<GenderPrediction>(response);

        if (genderPrediction != null && genderPrediction.Gender != null)
        {
            // Actualizar la UI según el género predicho
            if (genderPrediction.Gender == "male")
            {
                GenderBox.Color = Colors.Blue;
                ResultLabel.Text = $"El nombre {name} es masculino.";
            }
            else
            {
                GenderBox.Color = Colors.Pink;
                ResultLabel.Text = $"El nombre {name} es femenino.";
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo predecir el género.", "OK");
        }
    }
     public class GenderPrediction
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public double Probability { get; set; }
        public int Count { get; set; }
    }
}
