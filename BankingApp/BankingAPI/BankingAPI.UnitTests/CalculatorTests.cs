public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}


public class CalculatorTests
{
    // [Theory]
    // [InlineData(3, 4, 7)]
    // [InlineData(2, 5, 7)]
    // [InlineData(0, 0, 0)]
    public void Add_TwoNumbers_ReturnsSum(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(a, b);

        // Assert
        // Assert.Equal(expected, result);
    }
}