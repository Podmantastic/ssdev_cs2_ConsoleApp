using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp;

namespace Ssdev_Cs2_ConsoleAppTest;

[TestFixture]
public class OrderCalculatorTests
{
    private OrderDiscountCalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new OrderDiscountCalculator();
    }

    [Test]
    public void Do_SingleItemWithThreeOrMoreUnits_AppliesItemDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 40m)
        };

        // Act
        decimal discount = _calculator.Do(order);

        // Assert
        Assert.That(discount, Is.EqualTo(10m)); // 10% of unit price for 3 units
    }

    [Test]
    public void Do_OrderTotalOver100_AppliesOrderLevelDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 50m)
        };

        // Act
        decimal discount = _calculator.Do(order);

        // Assert
        Assert.That(discount, Is.EqualTo(12.5m)); // 10% of unit price + 5% of total order
    }

    [Test]
    public void Do_OrderTotalUnder100_OnlyAppliesItemDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 30m)
        };

        // Act
        decimal discount = _calculator.Do(order);

        // Assert
        Assert.That(discount, Is.EqualTo(3m)); // 10% of unit price
    }

    [Test]
    public void Do_MultipleItemsWithDifferentQuantities_CalculatesCorrectDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 40m),
            new Item("Product B", 2, 30m),
            new Item("Product C", 4, 20m)
        };

        // Act
        decimal discount = _calculator.Do(order);

        // Assert
        decimal expectedDiscount = 
            (40 * 0.10m) +  // Product A: 10% of unit price
            (20 * 0.10m) +  // Product C: 10% of unit price
            ((40 * 3) + (30 * 2) + (20 * 4)) * 0.05m; // 5% order level discount

        Assert.That(discount, Is.EqualTo(Math.Round(expectedDiscount, 2)));
    }

    [Test]
    public void Do_NullOrder_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.That(() => _calculator.Do(null), 
            Throws.ArgumentNullException);
    }

    [Test]
    public void Do_EmptyOrder_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.That(() => _calculator.Do(new List<Item>()), 
            Throws.ArgumentException);
    }

    [Test]
    public void Do_ItemWithNegativeUnitPrice_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, -10m)
        };

        // Act & Assert
        Assert.That(() => _calculator.Do(order), 
            Throws.ArgumentException.With.Message.Contains("Unit price"));
    }

    [Test]
    public void Do_ItemWithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", -3, 10m)
        };

        // Act & Assert
        Assert.That(() => _calculator.Do(order), 
            Throws.ArgumentException.With.Message.Contains("Quantity"));
    }

    [Test]
    public void Do_ItemWithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("", 3, 10m)
        };

        // Act & Assert
        Assert.That(() => _calculator.Do(order), 
            Throws.ArgumentException.With.Message.Contains("Item name"));
    }
}