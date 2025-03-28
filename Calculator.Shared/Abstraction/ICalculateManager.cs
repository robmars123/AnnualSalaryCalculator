namespace Calculator.Shared.Abstraction
{
    public interface ICalculateManager
    {
        string CalculateTotal(ICategoryType categryType, decimal percentIncreaseAmount, decimal yearly);
    }
}
