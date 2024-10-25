namespace Couteau.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void Nombre_Genero_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(Nombre_GeneroPage));
    }

    private void Nombre_Edad_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(Nombre_EdadPage));
    }

    private void Pais_Universidad_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(Pais_UniversidadPage));
    }

    private void Clima_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ClimaPage));
    }

    private void WordPress_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(WordPressPage));
    }

    private void Contactame_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ContactamePage));
    }
}