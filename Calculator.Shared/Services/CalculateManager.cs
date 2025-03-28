using Calculator.Shared.Abstraction;

namespace Calculator.Services
{
    public class CalculateManager : ICalculateManager
    {
        public string CalculateTotal(ICategoryType categryType, decimal percentIncreaseAmount, decimal yearly)
        {
            return categryType.CalculateTotal(percentIncreaseAmount, yearly);
        }
    }
}
