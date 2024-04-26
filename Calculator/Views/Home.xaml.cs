using Calculator.Services;
using Calculator.Shared.ViewModels;

namespace Calculator.Pages;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
        HandlerChanged += OnHandlerChanged;
    }
    void OnHandlerChanged(object sender, EventArgs e)
    {
        BindingContext = Handler.MauiContext.Services.GetService<HomeViewModel>();
    }
}