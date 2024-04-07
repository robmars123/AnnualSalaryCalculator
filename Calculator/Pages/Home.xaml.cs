using Calculator.Services;

namespace Calculator.Pages;

public partial class Home : ContentPage
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

    public Home()
    {
        InitializeComponent();
        PopulateControls();
    }

    private void PopulateControls()
    {
        decimal initialValue = 0;
        YearlyLabel.Text = YEARLY;
        MonthlyLabel.Text = MONTHLY;
        BiWeeklyLabel.Text = BIWEEKLY;
        WeeklyLabel.Text = WEEKLY;
        HourlyLabel.Text = HOURLY;
        DifferenceLabel.Text = DIFF;
        PercentIncreaseLabel.Text = PERCENTINCREASE;
        TotalIncreaseLabel.Text += TOTALINCREASE;

        MonthlyResult.Text = initialValue.ToString();
        BiWeeklyResult.Text = initialValue.ToString();
        WeeklyResult.Text = initialValue.ToString();
        HourlyResult.Text = initialValue.ToString();
    }
    private void ResetAll()
    {
        YearlyLabel.Text = YEARLY;
        MonthlyLabel.Text = MONTHLY;
        BiWeeklyLabel.Text = BIWEEKLY;
        WeeklyLabel.Text = WEEKLY;
        HourlyLabel.Text = HOURLY;
    }

    public void Calculate(decimal originalInput)
    {
        //total deductions in tax %
        decimal totalDeduction = 0;
        if (!string.IsNullOrEmpty(TaxEntry.Text))
        {
            tax = Convert.ToDecimal(TaxEntry.Text) / 100;
            totalDeduction = originalInput * tax;
        }
        yearly = originalInput - totalDeduction;

        //percent increase
        decimal totalIncrease = 0;
        if (!string.IsNullOrEmpty(PercentIncreaseEntry.Text) && PercentIncreaseEntry.Text != "0")
        {
            percentIncreaseAmount = (originalInput * Convert.ToDecimal(PercentIncreaseEntry.Text)) / 100;
        }
        totalIncrease = originalInput + percentIncreaseAmount;

        //new increase
        TotalIncreaseResult.Text = String.Format("{0:C}", totalIncrease);

        CalculateResult(percentIncreaseAmount, yearly);
        //difference
        DifferenceResult.Text = String.Format("{0:C}", (totalIncrease - originalInput));
    }

    private void CalculateResult(decimal percentIncreaseAmount, decimal yearly)
    {
        MonthlyResult.Text = $"{CalculateTotal("monthly", percentIncreaseAmount, yearly)}";
        BiWeeklyResult.Text = $"{CalculateTotal("biweekly", percentIncreaseAmount, yearly)}";
        WeeklyResult.Text = $"{CalculateTotal("weekly", percentIncreaseAmount, yearly)}";
        HourlyResult.Text = $"{CalculateTotal("hourly", percentIncreaseAmount, yearly)}";
    }

    public string CalculateTotal(string type, decimal percentIncreaseAmount, decimal yearly)
    {
        return CalculateManager.CalculateTotal(type, percentIncreaseAmount, yearly);
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {
        ResetAll();
        //annualy input
        decimal originalInput = 0;

        try
        {
            if (string.IsNullOrEmpty(YearlyEntry.Text) || YearlyEntry.Text == "")
            {
                YearlyEntry.Text = "0";
            }

            originalInput = Convert.ToDecimal(YearlyEntry.Text);

            Calculate(originalInput);
        }
        catch (Exception)
        {

        }

        SemanticScreenReader.Announce(MonthlyLabel.Text);
    }
}