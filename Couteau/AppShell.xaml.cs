using Couteau.Views;

namespace Couteau
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(Nombre_GeneroPage), typeof(Nombre_GeneroPage));
            Routing.RegisterRoute(nameof(Nombre_EdadPage), typeof(Nombre_EdadPage));
            Routing.RegisterRoute(nameof(Pais_UniversidadPage), typeof(Pais_UniversidadPage));
            Routing.RegisterRoute(nameof(ClimaPage), typeof(ClimaPage));
            Routing.RegisterRoute(nameof(WordPressPage), typeof(WordPressPage));
            Routing.RegisterRoute(nameof(ContactamePage), typeof(ContactamePage));
        }
    }
}
