using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp.Discounts;
using Ssdev_Cs2_ConsoleApp.DiscountStrategies;
using Ssdev_Cs2_ConsoleApp.DTO;

namespace Ssdev_Cs2_ConsoleAppTest.DiscountStrategies;

[TestFixture]
public class BulItemDiscountStrategyTests
{
    private BulItemDiscountStrategy _strategy = null!;
    
    [SetUp]
    public void Setup()
    {
        var actualDiscountOptions = new DiscountOptions();
        _strategy = new BulItemDiscountStrategy(actualDiscountOptions);
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
    public void CalculateDiscount_NoItemsWithQuantityThreeOrMore_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 1, 10.0m),
            new("Product B", 2, 20.0m)
        };
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(0m));
    }
    
    [Test]
    public void CalculateDiscount_OneItemWithQuantityThreeOrMore_Returns10PercentDiscount()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 3, 10.0m),
            new("Product B", 1, 20.0m)
        };
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(1.0m)); // 10% of 10.0 = 1.0
    }
    
    [Test]
    public void CalculateDiscount_MultipleItemsWithQuantityThreeOrMore_ReturnsSumOf10PercentDiscounts()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 3, 10.0m),
            new("Product B", 4, 20.0m),
            new("Product C", 2, 30.0m)
        };
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        Assert.That(discount, Is.EqualTo(3.0m)); // 10% of 10.0 + 10% of 20.0 = 1.0 + 2.0 = 3.0
    }
    
    [Test]
    public void CalculateDiscount_DecimalPrecision_HandlesDecimalsCorrectly()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Product A", 3, 9.99m),
            new("Product B", 5, 19.95m)
        };
        
        // Act
        var discount = _strategy.CalculateDiscount(order);
        
        // Assert
        var expectedDiscount = 9.99m * 0.10m + 19.95m * 0.10m;
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
