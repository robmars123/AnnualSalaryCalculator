namespace Calculator.Shared.Views;

public partial class Compounded
{
    public Compounded()
    {
        InitializeComponent();
    }

    private void CalculateCompoundClicked(object sender, EventArgs e)
    {
        // declare variables
        double ci, amount, yearlyInitial, increasePercent;
        int freq, numberOfYears, i;

        // take input of initial principal amount,
        // interest rate, periodicity of payment and year
        yearlyInitial = Convert.ToDouble(compoundYearlyInitialEntry.Text);
        increasePercent = Convert.ToDouble(compoundIncreasePercentlEntry.Text);
        freq = Convert.ToInt32(compoundFreqEntry.Text);
        numberOfYears = Convert.ToInt32(compoundNumberOfYearsEntry.Text);

        // initialize
        amount = yearlyInitial;

        // calculate compounded value using loop
        for (i = 0; i < freq * numberOfYears; i++)
            //formula = amound + (amount * (percent / (frequency * 100)))
            amount = amount + amount * (increasePercent / (freq * 100));

        //find the compound interest
        ci = Math.Round(amount, 2);

        // display result upto 2 decimal places
        CompoundTotal.Text = String.Format("{0:C}", Convert.ToDouble(ci)).ToString();
    }
}