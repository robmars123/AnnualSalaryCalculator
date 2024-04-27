using Calculator.Shared;

namespace Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Navigation();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 400;
            const int newHeight = 700;

            window.Width = newWidth;
            window.Height = newHeight;
            window.Title = "Annual Salary Calculator - MAUI";

            return window;
        }
    }
}
