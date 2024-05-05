

using Calculator.Shared.Enums;

namespace Calculator.Services
{
    public static class CalculateManager
    {
        public static string CalculateTotal(string type, decimal percentIncreaseAmount, decimal yearly)
        {
            if (type == nameof(CategoryType.Monthly))
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 12);
            }
            else if (type == nameof(CategoryType.BiWeekly))
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 26);
            }
            else if (type == nameof(CategoryType.Weekly))
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / 26 / 2);
            }
            else if (type == nameof(CategoryType.Hourly))
            {
                return string.Format("{0:C}", (percentIncreaseAmount + yearly) / (52 * 40));
            }
            else
                return "0";
        }
    }
}
