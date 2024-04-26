

namespace Calculator.Services
{
    public static class CalculateManager
    {
        public static string CalculateTotal(string type, decimal percentIncreaseAmount, decimal yearly)
        {
            if (type == "monthly")
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 12);
            }
            else if (type == "biweekly")
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 26);
            }
            else if (type == "weekly")
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 26 / 2);
            }
            else if (type == "hourly")
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / (52 * 40));
            }
            else
                return "0";
        }
    }
}
