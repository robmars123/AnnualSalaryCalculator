using Calculator.Shared.Models;
using Calculator.Shared.ViewModels;
using Calculator.Shared.Views.Base;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTelemetry.Trace;
using System.Text;

namespace Calculator.Shared.Views;

public partial class Home : BaseView
{
    private string _deviceToken;
    private readonly ILogger<MainPage> _logger;
    private readonly Tracer _tracer;

    // Parameterless constructor for Xamarin/Maui to use
    public Home()
    {
        InitializeComponent();
        _logger = null;  // Optionally initialize as null or use a fallback logger here
        _tracer = null;  // Optionally initialize as null
    }



    // Constructor with DI-injected dependencies
    public Home(ILogger<MainPage> logger, Tracer tracer)
    {
        InitializeComponent();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));

        // Debugging log to confirm if tracer is being set
        Console.WriteLine(_tracer != null ? "Tracer initialized" : "Tracer is null");
    }
}