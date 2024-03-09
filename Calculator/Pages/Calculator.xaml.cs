namespace Calculator.Pages;

public partial class Calculator : ContentPage
{
    public const string YEARLY = "Yearly:";
    public const string MONTHLY = "Monthly:";
    public const string BIWEEKLY = "BiWeekly:";
    public const string WEEKLY = "Weekly:";
    public const string HOURLY = "Hourly:";
    public const string DIFF = "Difference:";
    public const string PERCENTINCREASE = "Percent Increase:";
    public const string TOTALINCREASE = "Total Yearly:";

    decimal tax = 0.0M;
    decimal percentIncreaseAmount = 0.0M;
    decimal monthly = 0.0M, weekly = 0.0M;
    decimal hourly = 0.0M;
    decimal yearly = 0.0M;
    int count = 0;
    public Calculator()
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
        //monthly
        CalculateMonthly(percentIncreaseAmount, yearly);
        //biweekly
        CalculateBiWeekly(percentIncreaseAmount, yearly);
        //biweekly
        CalculateWeekly(percentIncreaseAmount, yearly);
        //hourly
        CalculateHourly(percentIncreaseAmount, yearly);
        //difference
        DifferenceResult.Text = String.Format("{0:C}", (totalIncrease - originalInput));
    }

    private void CalculateMonthly(decimal percentIncreaseAmount, decimal yearly)
    {
        MonthlyResult.Text = $"{CalculateTotal("monthly", percentIncreaseAmount, yearly)}";
    }
    private void CalculateBiWeekly(decimal percentIncreaseAmount, decimal yearly)
    {
        BiWeeklyResult.Text = $"{CalculateTotal("biweekly", percentIncreaseAmount, yearly)}";
    }

    private void CalculateWeekly(decimal percentIncreaseAmount, decimal yearly)
    {
        WeeklyResult.Text = $"{CalculateTotal("weekly", percentIncreaseAmount, yearly)}";
    }

    private void CalculateHourly(decimal percentIncreaseAmount, decimal yearly)
    {
        HourlyResult.Text = $"{CalculateTotal("hourly", percentIncreaseAmount, yearly)}";
    }
    public string CalculateTotal(string type, decimal percentIncreaseAmount, decimal yearly)
    {
        if (type == "monthly")
        {
            return String.Format("{0:C}", (percentIncreaseAmount + yearly) / 12);
        }
        else if (type == "biweekly")
        {
            return String.Format("{0:C}", (percentIncreaseAmount + yearly) / 26);
        }
        else if (type == "weekly")
        {
            return String.Format("{0:C}", ((percentIncreaseAmount + yearly) / 26) / 2);
        }
        else if (type == "hourly")
        {
            return String.Format("{0:C}", (percentIncreaseAmount + yearly) / (52 * 40));
        }
        else
            return "0";
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {
        ResetAll();
        //annualy input
        decimal originalInput = 0;

        try
        {
            //errorMsg.Text = "";

            if (string.IsNullOrEmpty(YearlyEntry.Text) || YearlyEntry.Text == "")
            {
                YearlyEntry.Text = "0";
            }
             //else if (YearlyEntry.Text == "0")
            //    errorMsg.Text = "Required " + yearlyAmount.Name;

            originalInput = Convert.ToDecimal(YearlyEntry.Text);

            Calculate(originalInput);
        }
        catch (Exception ex)
        {
            throw;
        }

        SemanticScreenReader.Announce(MonthlyLabel.Text);
    }
}