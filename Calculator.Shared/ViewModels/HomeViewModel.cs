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
        private decimal monthly = 0.0M, weekly = 0.0M;
        private decimal hourly = 0.0M;
        private decimal yearly = 0.0M;

        #region Property
        [ObservableProperty]
        private string _title = "Annual Salary";

        [ObservableProperty]
        private string? percentIncreaseEntry;

        [ObservableProperty]
        private CalculatorModel calculator = new CalculatorModel();

        #endregion Property
        public HomeViewModel()
        {
            InitiateControls();
        }

        public ICommand OnCalculate_Command => new Command(OnCalculate_Tapped);
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

        private void PopulateResultDefaultValues()
        {
            decimal initialValue = 0;
            Calculator.TotalIncreaseResult += initialValue.ToString();
            Calculator.MonthlyResult = initialValue.ToString();
            Calculator.BiWeeklyResult = initialValue.ToString();
            Calculator.WeeklyResult = initialValue.ToString();
            Calculator.HourlyResult = initialValue.ToString();
        }
        private void ResetAll()
        {

        }

        public void Calculate(decimal originalInput)
        {
            //total deductions in tax %
            decimal totalDeduction = 0;
            if (!string.IsNullOrEmpty(Calculator.TaxEntry))
            {
                tax = Convert.ToDecimal(Calculator.TaxEntry) / 100;
                totalDeduction = originalInput * tax;
            }
            yearly = originalInput - totalDeduction;

            //percent increase
            decimal totalIncrease = 0;
            if (!string.IsNullOrEmpty(Calculator.PercentIncreaseEntry) && Calculator.PercentIncreaseEntry != "0")
            {
                percentIncreaseAmount = (originalInput * Convert.ToDecimal(Calculator.PercentIncreaseEntry)) / 100;
            }
            totalIncrease = originalInput + percentIncreaseAmount;

            //new increase
            Calculator.TotalIncreaseResult = String.Format("{0:C}", totalIncrease);

            CalculateResult(percentIncreaseAmount, yearly);
            //difference
            Calculator.DifferenceResult = String.Format("{0:C}", (totalIncrease - originalInput));
        }

        private void CalculateResult(decimal percentIncreaseAmount, decimal yearly)
        {
            Calculator.MonthlyResult = $"{CalculateTotal("monthly", percentIncreaseAmount, yearly)}";
            Calculator.BiWeeklyResult = $"{CalculateTotal("biweekly", percentIncreaseAmount, yearly)}";
            Calculator.WeeklyResult = $"{CalculateTotal("weekly", percentIncreaseAmount, yearly)}";
            Calculator.HourlyResult = $"{CalculateTotal("hourly", percentIncreaseAmount, yearly)}";
        }

        public string CalculateTotal(string type, decimal percentIncreaseAmount, decimal yearly)
        {
            return CalculateManager.CalculateTotal(type, percentIncreaseAmount, yearly);
        }
        private void OnCalculate_Tapped()
        {
            ResetAll();
            //annualy input
            decimal originalInput = 0;

            try
            {
                if (string.IsNullOrEmpty(Calculator.YearlyEntry) || Calculator.YearlyEntry == "")
                {
                    Calculator.YearlyEntry = "0";
                }

                originalInput = Convert.ToDecimal(Calculator.YearlyEntry);

                Calculate(originalInput);
            }
            catch (Exception)
            {
                //todo: Write error message here.
            }
        }
    }
}
