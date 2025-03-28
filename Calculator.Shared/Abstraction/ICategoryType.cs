namespace Calculator.Shared.Abstraction
{
    public interface ICategoryType
    {
        string CalculateTotal(decimal percentIncreaseAmount, decimal yearly);
    }
}
