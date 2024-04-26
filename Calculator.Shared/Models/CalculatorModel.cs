using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.Shared.Models
{
    public class CalculatorModel : BindableObject
    {
        #region Labels
        public string YearlyLabel { get; set; }
        public string MonthlyLabel { get; set; }
        public string BiWeeklyLabel { get; set; }
        public string WeeklyLabel { get; set; }
        public string HourlyLabel { get; set; }

        #endregion Labels

        #region Totals
        public string DifferenceLabel { get; set; }
        public string PercentIncreaseLabel { get; set; }
        public string TotalIncreaseLabel { get; set; }
        #endregion
        #region Result
        public static readonly BindableProperty TotalIncreaseResultProperty =
            BindableProperty.Create("TotalIncreaseResult", typeof(string), typeof(CalculatorModel), null);
        public string TotalIncreaseResult
        {
            get { return (string)GetValue(TotalIncreaseResultProperty); }
            set { SetValue(TotalIncreaseResultProperty, value); }
        }
        public static readonly BindableProperty DifferenceResultProperty =
    BindableProperty.Create("DifferenceResult", typeof(string), typeof(CalculatorModel), null);
        public string DifferenceResult
        {
            get { return (string)GetValue(DifferenceResultProperty); }
            set { SetValue(DifferenceResultProperty, value); }
        }

        public static readonly BindableProperty MonthlyResultProperty =
                    BindableProperty.Create("MonthlyResult", typeof(string), typeof(CalculatorModel), null);
        public string MonthlyResult
        {
            get { return (string)GetValue(MonthlyResultProperty); }
            set { SetValue(MonthlyResultProperty, value); }
        }
        public static readonly BindableProperty BiWeeklyResultProperty =
            BindableProperty.Create("BiWeeklyResult", typeof(string), typeof(CalculatorModel), null);
        public string BiWeeklyResult
        {
            get { return (string)GetValue(BiWeeklyResultProperty); }
            set { SetValue(BiWeeklyResultProperty, value); }
        }
        public static readonly BindableProperty WeeklyResultProperty =
            BindableProperty.Create("WeeklyResult", typeof(string), typeof(CalculatorModel), null);
        public string WeeklyResult
        {
            get { return (string)GetValue(WeeklyResultProperty); }
            set { SetValue(WeeklyResultProperty, value); }
        }
        public static readonly BindableProperty HourlyResultProperty =
            BindableProperty.Create("HourlyResult", typeof(string), typeof(CalculatorModel), null);
        public string HourlyResult
        {
            get { return (string)GetValue(HourlyResultProperty); }
            set { SetValue(HourlyResultProperty, value); }
        }
        #endregion Result

        #region Entry
        public string YearlyEntry { get; set; }
        public string TaxEntry { get; set; }
        public string PercentIncreaseEntry { get; set; }
        #endregion Entry
    }
}
