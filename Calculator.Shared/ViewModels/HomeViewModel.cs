using CommunityToolkit.Mvvm.ComponentModel;
using Calculator.Services;
using Calculator.Shared;
using Calculator.Shared.Models;
using System.Windows.Input;

namespace Calculator.Shared.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private const string YEARLY = "Yearly:";
        private const string MONTHLY = "Monthly:";
        private const string BIWEEKLY = "BiWeekly:";
        private const string WEEKLY = "Weekly:";
        private const string HOURLY = "Hourly:";
        private const string DIFF = "Difference:";
        private const string PERCENTINCREASE = "Percent Increase:";
        private const string TOTALINCREASE = "Total Yearly:";

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

        #endregion Property
        #region Constructor
        public HomeViewModel()
        {
            InitiateControls();
        }
        #endregion Constructor
        #region Commands
        public ICommand OnCalculate_Command => new Command(OnCalculate_Tapped);
        #endregion Commands

        #region Initiate
        private void InitiateControls()
        {
            Calculator.YearlyLabel = YEARLY;
            Calculator.MonthlyLabel = MONTHLY;
            Calculator.BiWeeklyLabel = BIWEEKLY;
            Calculator.WeeklyLabel = WEEKLY;
            Calculator.HourlyLabel = HOURLY;

            Calculator.PercentIncreaseLabel = PERCENTINCREASE;
            Calculator.TotalIncreaseLabel = TOTALINCREASE;
            Calculator.DifferenceLabel = DIFF;

            PopulateResultDefaultValues();
        }
        #endregion
        #region Methods
        private void PopulateResultDefaultValues()
        {
            decimal initialValue = 0;
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

        public void Calculate(decimal originalInput)
        {
            //Percent Increase
            totalIncrease = GetPercentIncreaseTotal(originalInput);
            //Calculate yearly with/without tax deduction
            yearly = GetYearlyAfterTax(originalInput);

            //Display Results
            CalculateResult(percentIncreaseAmount, totalIncrease, originalInput, yearly);
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
            Calculator.MonthlyResult = $"{CalculateManager.CalculateTotal("monthly", percentIncreaseAmount, yearly)}";
            Calculator.BiWeeklyResult = $"{CalculateManager.CalculateTotal("biweekly", percentIncreaseAmount, yearly)}";
            Calculator.WeeklyResult = $"{CalculateManager.CalculateTotal("weekly", percentIncreaseAmount, yearly)}";
            Calculator.HourlyResult = $"{CalculateManager.CalculateTotal("hourly", percentIncreaseAmount, yearly)}";

            Calculator.DifferenceResult = string.Format("{0:C}", (totalIncrease - originalInput));
            Calculator.TotalIncreaseResult = totalIncrease.ToString();
        }
        private void OnCalculate_Tapped()
        {
            ResetAll();

            try
            {
                if (string.IsNullOrEmpty(Calculator.YearlyEntry) || Calculator.YearlyEntry == "")
                    Calculator.YearlyEntry = "0";

                decimal originalInput = Convert.ToDecimal(Calculator.YearlyEntry);
                Calculate(originalInput);
            }
            catch (Exception)
            {
                //TODO: Write error message here.
            }
        }
        #endregion Methods
    }
}
