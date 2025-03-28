using Calculator.Shared.Abstraction;

namespace Calculator.Shared.Models
{
    public class Hourly : ICategoryType
    {
        public string CalculateTotal(decimal percentIncreaseAmount, decimal yearly)
        {
            return string.Format("{0:C}", (percentIncreaseAmount + yearly) / (52 * 40));
        }
    }
}
