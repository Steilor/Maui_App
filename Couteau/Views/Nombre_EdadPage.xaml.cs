using Newtonsoft.Json;


namespace Couteau.Views;

public partial class Nombre_EdadPage : ContentPage
{
	private readonly HttpClient _httpClient = new HttpClient();
	public Nombre_EdadPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		string name = NameEntry.Text;
		if (String.IsNullOrEmpty(name))
		{
            await DisplayAlert("Error", "Ingrese un nombre por favor. ", "OK");
			return;
        }

        //Llamar a la Api
        string url = $"https://api.agify.io/?name={name}";      
        var response = await _httpClient.GetStringAsync(url);

		// Deserialización de la respuesta
		var edadPrediccion = JsonConvert.DeserializeObject<AgePrediction>(response);

		if(edadPrediccion != null && edadPrediccion.Age.HasValue)
		{
			int edad = edadPrediccion.Age.Value;
			AgeLabel.Text = $"Edad predicha: {edad} años";

			if(edad < 18)
			{
				CategoryLabel.Text = "Usted es Joven";
				ImagenEdad.Source = "joven.jpg";
				ImagenEdad.IsVisible = true;
			}
			else if(edad >= 18 & edad < 60)
			{
				CategoryLabel.Text = "Usted es Adulto";
				ImagenEdad.Source = "adulto.jpg";
                ImagenEdad.IsVisible = true;
            }
			else
			{
				CategoryLabel.Text = "Usted es anciano";
				ImagenEdad.Source = "anciano.jpg";
                ImagenEdad.IsVisible = true;
            }
		}
		else
		{
            await DisplayAlert("Error", "No se pudo predecir la edad.", "OK");
        }
    }
    public class AgePrediction
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public int Count { get; set; }
    }
}