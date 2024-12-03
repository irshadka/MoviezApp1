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
			.UseMauiCommunityToolkit()
			.RegisterAppservices()
			.RegisterViewModels()
			.RegisterViews()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
			});
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
		mauiAppBuilder.Services.AddTransient<MovieDetailpageViewModel>();
		mauiAppBuilder.Services.AddTransient<SearchPageViewModel>();
        mauiAppBuilder.Services.AddTransient<CastDetailPageViewModel>();
        return mauiAppBuilder;
	}
	public static MauiAppBuilder RegisterAppservices(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<IMoviezAPIService, MoviezAPIService>();
		return mauiAppBuilder;
	}
	public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<HomePage>();
		mauiAppBuilder.Services.AddTransient<MovieDetailPage>();
		mauiAppBuilder.Services.AddTransient<SearchPage>();
        mauiAppBuilder.Services.AddTransient<CastDetailPage>();
        return mauiAppBuilder;
	}
}
