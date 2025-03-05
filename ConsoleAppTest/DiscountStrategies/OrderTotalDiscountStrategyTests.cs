using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp.Discounts;
using Ssdev_Cs2_ConsoleApp.DiscountStrategies;
using Ssdev_Cs2_ConsoleApp.DTO;

namespace Ssdev_Cs2_ConsoleAppTest.DiscountStrategies;

[TestFixture]
public class OrderTotalDiscountStrategyTests
{
    private OrderTotalDiscountStrategy _strategy = null!;
    
    [SetUp]
    public void Setup()
    {
         var actualDiscountOptions = new DiscountOptions();
        _strategy = new OrderTotalDiscountStrategy(actualDiscountOptions);
    }
    
    [Test]
    public void CalculateDiscount_EmptyOrder_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>();
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0m));
    }
    
    [Test]
    public void CalculateDiscount_OrderTotalBelow100_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 1, 50.0m),
            new("Product B", 2, 20.0m)
        };
        // Total: 50 + (2*20) = 90, which is < 100
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0m));
    }
    
    [Test]
    public void CalculateDiscount_OrderTotalExactly100_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 2, 50.0m)
        };
        // Total: 2*50 = 100, which is == 100
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0m));
    }
    
    [Test]
    public void CalculateDiscount_OrderTotalGreaterThan100_Returns5PercentDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 2, 60.0m)
        };
        // Total: 2*60 = 120, which is > 100
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(6.0m)); // 5% of 120 = 6.0
    }
    
    [Test]
    public void CalculateDiscount_MultipleItemsWithTotalGreaterThan100_Returns5PercentOfTotal()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 3, 20.0m),
            new("Product B", 2, 30.0m),
            new("Product C", 1, 15.0m)
        };
        // Total: (3*20) + (2*30) + (1*15) = 60 + 60 + 15 = 135, which is > 100
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(6.75m)); // 5% of 135 = 6.75
    }
    
    [Test]
    public void CalculateDiscount_DecimalPrecision_HandlesDecimalsCorrectly()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 4, 24.99m),
            new("Product B", 1, 9.95m)
        };
        // Total: (4*24.99) + (1*9.95) = 99.96 + 9.95 = 109.91, which is > 100
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        var expectedDiscount = 109.91m * 0.05m;
        Assert.That(discount, Is.EqualTo(expectedDiscount));
    }
    
    [Test]
    public void CalculateDiscount_OrderWithNullItems_HandlesNullsGracefully()
    {
        // Arrange
        List<Item>? order = null;

        // Act & Assert
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.That(() => _strategy.CalculateDiscount(order), Throws.ArgumentNullException);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
