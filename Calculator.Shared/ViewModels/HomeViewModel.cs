using Calculator.Services;
using Calculator.Shared.Abstraction;
using Calculator.Shared.Diagnostics;
using Calculator.Shared.Enums;
using Calculator.Shared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.Windows.Input;

namespace Calculator.Shared.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ICalculateManager _calculateManager;
        public HomeViewModel(ICalculateManager calculateManager)
        {
            _calculateManager = calculateManager;
        }
        private decimal tax = 0.0M;
        private decimal percentIncreaseAmount = 0.0M;
        private decimal yearly = 0.0M;
        private decimal totalIncrease = 0.0M;

        #region Property
        [ObservableProperty]
        private string _title = "Annual Salary";

        [ObservableProperty]
        private string? percentIncreaseEntry;

        [ObservableProperty]
        private CalculatorModel calculator = new CalculatorModel();
        private readonly ILogger<HomeViewModel> _logger;
        private readonly Tracer _tracer;


        #endregion Property
        #region Constructor
        public HomeViewModel()
        {
            InitiateControls();
        }

        public HomeViewModel(ILogger<HomeViewModel> logger, Tracer tracer)
        {
            _logger = logger;
            _tracer = tracer;
        }
        #endregion Constructor
        #region Commands
        public ICommand OnCalculate_Command => new Command(OnCalculate_Tapped);

        #endregion Commands

        #region Initiate
        private void InitiateControls()
        {
            PopulateResultDefaultValues();
        }
        #endregion
        #region Methods
        private void PopulateResultDefaultValues()
        {
            decimal initialValue = 0;
            tax = default;
            Calculator.TotalIncreaseResult = initialValue.ToString();
            Calculator.DifferenceResult = initialValue.ToString();
            Calculator.MonthlyResult = initialValue.ToString();
            Calculator.BiWeeklyResult = initialValue.ToString();
            Calculator.WeeklyResult = initialValue.ToString();
            Calculator.HourlyResult = initialValue.ToString();
        }
        private void ResetAll()
        {
            PopulateResultDefaultValues();
        }

        public void Calculate(Action<string> callBack, decimal originalInput)
        {
            totalIncrease = GetPercentIncreaseTotal(originalInput);
            yearly = GetYearlyAfterTax(originalInput);

            //Display Results
            CalculateResult(percentIncreaseAmount, totalIncrease, originalInput, yearly);

            callBack(Calculator.HistoryList.FirstOrDefault());
        }

        private decimal GetPercentIncreaseTotal(decimal originalInput)
        {
            if (!string.IsNullOrEmpty(Calculator.PercentIncreaseEntry) && Calculator.PercentIncreaseEntry != "0")
                percentIncreaseAmount = (originalInput * Convert.ToDecimal(Calculator.PercentIncreaseEntry)) / 100;
            else
                percentIncreaseAmount = 0;

            return originalInput + percentIncreaseAmount;
        }

        private decimal GetYearlyAfterTax(decimal originalInput)
        {
            if (!string.IsNullOrEmpty(Calculator.TaxEntry))
            {
                tax = Convert.ToDecimal(Calculator.TaxEntry) / 100;
            }
            return originalInput - (originalInput * tax);
        }

        private void CalculateResult(decimal percentIncreaseAmount, decimal totalIncrease, decimal originalInput, decimal yearly)
        {
            Calculator.MonthlyResult = _calculateManager.CalculateTotal(new Monthly(), percentIncreaseAmount, yearly);
            Calculator.BiWeeklyResult = _calculateManager.CalculateTotal(new BiWeekly(), percentIncreaseAmount, yearly);
            Calculator.WeeklyResult = _calculateManager.CalculateTotal(new Weekly(), percentIncreaseAmount, yearly);
            Calculator.HourlyResult = _calculateManager.CalculateTotal(new Hourly(), percentIncreaseAmount, yearly);

            Calculator.DifferenceResult = string.Format("{0:C}", totalIncrease - originalInput);
            Calculator.TotalIncreaseResult = string.Format("{0:C}", totalIncrease);
        }
        private void OnCalculate_Tapped()
        {

            ResetAll();
            if (string.IsNullOrEmpty(Calculator.YearlyEntry) || Calculator.YearlyEntry == "")
                Calculator.YearlyEntry = "0";

            decimal originalInput = Convert.ToDecimal(Calculator.YearlyEntry);
            //callbacks
            Calculate((returnedMsg) => Calculator.HistoryList.FirstOrDefault(), originalInput);

        }
        #endregion Methods
    }
}
