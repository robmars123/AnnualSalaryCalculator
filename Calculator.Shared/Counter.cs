namespace Calculator.Shared
{
    // All the code in this file is included in all platforms.
    public class Counter
    {
        int count = 0;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            Button CounterBtn = (Button)sender;
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
