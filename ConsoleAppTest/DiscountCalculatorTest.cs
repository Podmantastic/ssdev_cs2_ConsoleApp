using NUnit.Framework;
using Ssdev_Cs2_ConsoleApp;

namespace ConsoleAppTest;

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
    public void CalculateTotalPrice_NullOrder_ThrowsArgumentException()
    {
        List<Item> order = null!;
        Assert.That(() => _sut.Do(order), Throws.ArgumentException.With.Message.EqualTo("The order list cannot be null."));
    }

    [Test]
    public void CalculateTotalPrice_EmptyOrder_ReturnsZero()
    {
        var order = new List<Item>();
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(0.00m));
    }

    [Test]
    public void CalculateTotalPrice_NullItemInOrder_ThrowsArgumentException()
    {
        var order = new List<Item> { null! };
        Assert.That(() => _sut.Do(order), Throws.ArgumentException.With.Message.EqualTo("The order list contains a null item."));
    }

    [Test]
    public void CalculateTotalPrice_NegativeQuantity_ThrowsArgumentException()
    {
        var order = new List<Item> { new("Laptop", -1, 1000.00m) };
        Assert.That(() => _sut.Do(order), Throws.ArgumentException.With.Message.EqualTo("Quantity and unit price must be non-negative."));
    }

    [Test]
    public void CalculateTotalPrice_NegativeUnitPrice_ThrowsArgumentException()
    {
        var order = new List<Item> { new("Laptop", 1, -1000.00m) };
        Assert.That(() => _sut.Do(order), Throws.ArgumentException.With.Message.EqualTo("Quantity and unit price must be non-negative."));
    }

    [Test]
    public void CalculateTotalPrice_NoDiscounts_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new("Laptop", 1, 1000.00m),
            new("Mouse", 2, 25.00m),
            new("Keyboard", 1, 50.00m)
        };
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(1045.00m));
    }

    [Test]
    public void CalculateTotalPrice_QuantityDiscountForOneItem_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new("Laptop", 1, 1000.00m),
            new("Mouse", 3, 25.00m), // 10% discount
            new("Keyboard", 2, 50.00m)
        };
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(1109.12m));
    }

    [Test]
    public void CalculateTotalPrice_QuantityDiscountForAllItems_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new("Laptop", 3, 1000.00m), // 10% discount
            new("Mouse", 3, 25.00m),     // 10% discount
            new("Keyboard", 3, 50.00m)  // 10% discount
        };
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(2757.37m));
    }

    [Test]
    public void CalculateTotalPrice_TotalExactly100_NoOrderDiscount()
    {
        var order = new List<Item>
        {
            new("Laptop", 1, 100.00m)
        };
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(100.00m));
    }

    [Test]
    public void CalculateTotalPrice_TotalAbove100_AppliesOrderDiscount()
    {
        var order = new List<Item>
        {
            new("Laptop", 1, 101.00m)
        };
        var totalPrice = _sut.Do(order);
        Assert.That(totalPrice, Is.EqualTo(95.95m));
    }

        [Test]
    public void CalculateTotalPrice_ShouldHandleExactFractionalPart()
    {
        // Arrange
        var order = new List<Item>
        {
            new("Test Item", 1, 100.125m) // Total price will be 100.125
        };

        // Act
        var totalPrice = _sut.Do(order);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(95.12m));
    }
}