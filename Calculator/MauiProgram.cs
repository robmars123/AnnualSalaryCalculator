using Calculator.Pages;
using Calculator.Shared.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            // The important part
            builder.Services.Scan(s => s
                .FromAssemblyOf<App>()
                .AddClasses(f => f.AssignableToAny(
                        typeof(ContentPage),
                        typeof(ObservableObject))
                    )
                    .AsSelf()
                    .WithSingletonLifetime()
            );
            // end of important part

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddSingleton<Home>();
            builder.Services.AddSingleton<Navigation>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
