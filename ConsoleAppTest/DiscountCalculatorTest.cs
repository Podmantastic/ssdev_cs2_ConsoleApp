using ConsoleApp;
using NUnit.Framework;

[TestFixture]
public class OrderDiscountCalculatorTests
{
    private OrderDiscountCalculator _orderDiscountCalculator = null!;

    [SetUp]
    public void Setup()
    {
        // Initialize the OrderDiscountCalculator before each test
        _orderDiscountCalculator = new OrderDiscountCalculator();
    }

    [Test]
    public void CalculateTotalPrice_NoItems_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>();

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateTotalPrice_ItemsWithoutDiscounts_ReturnsCorrectTotal()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Laptop", 1, 1000m),
            new("Keyboard", 2, 50m)
        };

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(1100m)); // 1000 + (2 * 50) = 1100
    }

    [Test]
    public void CalculateTotalPrice_ItemsWithQuantityDiscount_ReturnsCorrectTotal()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Mouse", 3, 25m) // 10% discount on 3 mice
        };

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(67.5m)); // (3 * 25) * 0.9 = 67.5
    }

    [Test]
    public void CalculateTotalPrice_OrderWithBulkDiscount_ReturnsCorrectTotal()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Laptop", 1, 1000m),
            new("Mouse", 3, 25m), // 10% discount on mice
            new("Keyboard", 2, 50m)
        };

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(1109.125m)); // 1000 + 67.5 + 100 = 1167.5; 1167.5 * 0.95 = 1109.125
    }

    [Test]
    public void CalculateTotalPrice_TotalExactly100_NoBulkDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Item1", 2, 50m) // Total = 100
        };

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(100m)); // No bulk discount applied
    }

    [Test]
    public void CalculateTotalPrice_AllItemsWithDiscounts_ReturnsCorrectTotal()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Item1", 3, 100m), // 10% discount
            new("Item2", 4, 50m)   // 10% discount
        };

        // Act
        var totalPrice = _orderDiscountCalculator.CalculateTotalPrice(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(427.5m)); // (300 * 0.9) + (200 * 0.9) = 270 + 180 = 450; 450 * 0.95 = 427.5
    }
}