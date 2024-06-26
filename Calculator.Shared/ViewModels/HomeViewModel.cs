﻿using Calculator.Services;
using Calculator.Shared.Enums;
using Calculator.Shared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Calculator.Shared.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
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
            Calculator.MonthlyResult = $"{CalculateManager.CalculateTotal(nameof(CategoryType.Monthly), percentIncreaseAmount, yearly)}";
            Calculator.BiWeeklyResult = $"{CalculateManager.CalculateTotal(nameof(CategoryType.BiWeekly), percentIncreaseAmount, yearly)}";
            Calculator.WeeklyResult = $"{CalculateManager.CalculateTotal(nameof(CategoryType.Weekly), percentIncreaseAmount, yearly)}";
            Calculator.HourlyResult = $"{CalculateManager.CalculateTotal(nameof(CategoryType.Hourly), percentIncreaseAmount, yearly)}";

            Calculator.DifferenceResult = string.Format("{0:C}", totalIncrease - originalInput);
            Calculator.TotalIncreaseResult = string.Format("{0:C}", totalIncrease);
        }
        private void OnCalculate_Tapped()
        {
            ResetAll();

            try
            {
                if (string.IsNullOrEmpty(Calculator.YearlyEntry) || Calculator.YearlyEntry == "")
                    Calculator.YearlyEntry = "0";

                decimal originalInput = Convert.ToDecimal(Calculator.YearlyEntry);
                //callbacks
                Calculate((returnedMsg) => Calculator.HistoryList.FirstOrDefault(), originalInput);
            }
            catch (Exception)
            {
                //TODO: Write error message here.
            }
        }
        #endregion Methods
    }
}
