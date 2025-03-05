using Moq;
using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp;

namespace Ssdev_Cs2_ConsoleAppTest;

[TestFixture]
public class OrderProcessorTests
{
    [Test]
    public void Do_ShouldCallCalculateTotalPrice_WithCorrectOrder()
    {
        // Arrange
        var mockDiscountCalculator = new Mock<IOrderDiscountCalculator>();
        var orderProcessor = new OrderProcessor(mockDiscountCalculator.Object);

        List<Item> expectedOrder =
        [
            new("Laptop", 1, 1000.00m),
            new("Mouse", 3, 25.00m),
            new("Keyboard", 2, 50.00m)
        ];

        mockDiscountCalculator
            .Setup(calculator => calculator.Do(It.IsAny<List<Item>>()))
            .Returns(1109.12m); // Mock the return value

        // Act
        orderProcessor.Do();

        // Assert
        mockDiscountCalculator.Verify(
            calculator => calculator.Do(It.Is<List<Item>>(order =>
                order.Count == expectedOrder.Count &&
                order[0].Name == expectedOrder[0].Name &&
                order[1].Name == expectedOrder[1].Name &&
                order[2].Name == expectedOrder[2].Name)),
            Times.Once);

        // Alternatively, using Assert.That
        Assert.That(mockDiscountCalculator.Invocations.Count, Is.EqualTo(1), "CalculateTotalPrice should be called once.");
    }

    [Test]
    public void Do_ShouldWriteCorrectTotalPriceToConsole()
    {
        // Arrange
        var mockDiscountCalculator = new Mock<IOrderDiscountCalculator>();
        var orderProcessor = new OrderProcessor(mockDiscountCalculator.Object);

        var expectedTotalPrice = 1109.12m;
        mockDiscountCalculator
            .Setup(calculator => calculator.Do(It.IsAny<List<Item>>()))
            .Returns(expectedTotalPrice); // Mock the return value

        using var consoleOutput = new ConsoleOutput();
        // Act
        orderProcessor.Do();

        // Assert
        string consoleText = consoleOutput.GetOutput();
        Assert.That(consoleText.Contains("Order total after discount: $65.88"), "Console output should start with the expected total price.");
    }
}

// Helper class to capture console output
public class ConsoleOutput : IDisposable
{
    private readonly StringWriter stringWriter;
    private readonly TextWriter originalOutput;

    public ConsoleOutput()
    {
        stringWriter = new StringWriter();
        originalOutput = Console.Out;
        Console.SetOut(stringWriter);
    }

    public string GetOutput()
    {
        return stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(originalOutput);
        stringWriter.Dispose();
    }
}