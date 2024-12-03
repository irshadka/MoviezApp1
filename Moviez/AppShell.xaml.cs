using Moviez.Views;

namespace Moviez;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}
	private void RegisterRoutes()
	{
		Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
        Routing.RegisterRoute(nameof(CastDetailPage), typeof(CastDetailPage));
    }
}
