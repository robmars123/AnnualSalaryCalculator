using Calculator.Services;
using Calculator.Shared.Abstraction;
using Calculator.Shared.Models;
using Calculator.Shared.ViewModels;
using Calculator.Shared.Views;
using Microsoft.Extensions.Logging;

namespace Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
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
            builder.Services.AddScoped<ICalculateManager, CalculateManager>();
            builder.Services.AddScoped<ICategoryType, Monthly>();
            builder.Services.AddScoped<ICategoryType, BiWeekly>();
            builder.Services.AddScoped<ICategoryType, Weekly>();
            builder.Services.AddScoped<ICategoryType, Hourly>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
