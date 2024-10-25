namespace Couteau.Views;

public partial class ContactamePage : ContentPage
{
	public ContactamePage()
	{
		InitializeComponent();
	}
    private async void EnviarEmail_Clicked(object sender, EventArgs e)
    {
        try
        {
            var emailMessage = new EmailMessage
            {
                Subject = "Contacto desde la app",
                Body = "Hola, me gustaría contactarte para discutir un proyecto.",
                To = new List<string> { "esteilorpaniaguamateo@gmail.com" }
            };
            await Email.ComposeAsync(emailMessage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo enviar el correo: {ex.Message}", "OK");
        }
    }
}