using Camera.MAUI;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Moviez.Interfaces;
using Moviez.Services;
using CommunityToolkit.Maui;
using Moviez.ViewModels;
using Moviez.Views;

namespace Moviez;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseFFImageLoading()
			.UseMauiCameraView()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IMoviezAPIService, MoviezAPIService>();
		builder.Services.AddTransient<HomePage, HomePageViewModel>();
		builder.Services.AddTransient<MovieDetailPage, MovieDetailpageViewModel>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
