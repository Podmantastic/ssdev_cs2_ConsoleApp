using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp;

namespace Ssdev_Cs2_ConsoleAppTest;

[TestFixture]
public class OrderCalculatorTests
{
    private IOrderDiscountCalculator _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _sut = new OrderDiscountCalculator();
    }

    [Test]
    public void Do_OrderWithThreeOrMoreUnitsOfSameItem_AppliesItemDiscount()
    {
        // Arrange
        List<Item> items =
        [
            new("Product A", 3, 40.00m)
        ];

        // Act
        decimal discount = _sut.Do(items);

        // Assert
        Assert.That(discount, Is.EqualTo(12m)); // 10% of (40 * 3) = 12
    }

    [Test]
    public void Do_OrderTotalOver100_AppliesOrderLevelDiscount()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "Product A", UnitPrice = 50, Quantity = 3 }
        };

        // Act
        decimal discount = _sut.Do(Items);

        // Assert
        Assert.That(discount, Is.EqualTo(22.5m)); // 10% item discount (15) + 5% order discount (7.5)
    }

    [Test]
    public void Do_OrderTotalUnder100_OnlyAppliesItemDiscount()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "Product A", UnitPrice = 30, Quantity = 3 }
        };

        // Act
        decimal discount = _sut.Do(Items);

        // Assert
        Assert.That(discount, Is.EqualTo(9m)); // 10% of (30 * 3) = 9
    }

    [Test]
    public void Do_MultipleItemsWithDifferentQuantities_CalculatesCorrectDiscount()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "Product A", UnitPrice = 40, Quantity = 3 },
            new Item { Name = "Product B", UnitPrice = 30, Quantity = 2 },
            new Item { Name = "Product C", UnitPrice = 20, Quantity = 4 }
        };

        // Act
        decimal discount = _sut.Do(Items);

        // Assert
        decimal expectedItemDiscounts = 
            40 * 3 * 0.10m +  // Product A: 10% off 3 units
            20 * 4 * 0.10m +  // Product C: 10% off 4 units
            (40 * 3 + 30 * 2 + 20 * 4) * 0.05m; // 5% order level discount

        Assert.That(discount, Is.EqualTo(Math.Round(expectedItemDiscounts, 2)));
    }

    [Test]
    public void Do_NullItems_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.That(() => _sut.Do(null), 
            Throws.ArgumentNullException);
    }

    [Test]
    public void Do_EmptyItems_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.That(() => _sut.Do([]), 
            Throws.ArgumentException);
    }

    [Test]
    public void Do_NegativeUnitPrice_ThrowsArgumentException()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "Product A", UnitPrice = -10, Quantity = 3 }
        };

        // Act & Assert
        Assert.That(() => _sut.Do(Items), 
            Throws.ArgumentException.With.Message.Contains("Unit price"));
    }

    [Test]
    public void Do_NegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "Product A", UnitPrice = 10, Quantity = -3 }
        };

        // Act & Assert
        Assert.That(() => _sut.Do(Items), 
            Throws.ArgumentException.With.Message.Contains("Quantity"));
    }

    [Test]
    public void Do_EmptyItemName_ThrowsArgumentException()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { Name = "", UnitPrice = 10, Quantity = 3 }
        };

        // Act & Assert
        Assert.That(() => _sut.Do(Items), 
            Throws.ArgumentException.With.Message.Contains("Item name"));
    }
}