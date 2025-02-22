using ConsoleApp;
using NUnit.Framework;

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
        List<Item> order = null;
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateTotalPrice(order));
        Assert.That(exception.Message, Is.EqualTo("The order list cannot be null."));
    }

    [Test]
    public void CalculateTotalPrice_EmptyOrder_ReturnsZero()
    {
        var order = new List<Item>();
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(0.00m));
    }

    [Test]
    public void CalculateTotalPrice_NullItemInOrder_ThrowsArgumentException()
    {
        var order = new List<Item> { null };
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateTotalPrice(order));
        Assert.That(exception.Message, Is.EqualTo("The order list contains a null item."));
    }

    [Test]
    public void CalculateTotalPrice_NegativeQuantity_ThrowsArgumentException()
    {
        var order = new List<Item> { new Item("Laptop", -1, 1000.00m) };
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateTotalPrice(order));
        Assert.That(exception.Message, Is.EqualTo("Quantity and unit price must be non-negative."));
    }

    [Test]
    public void CalculateTotalPrice_NegativeUnitPrice_ThrowsArgumentException()
    {
        var order = new List<Item> { new Item("Laptop", 1, -1000.00m) };
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateTotalPrice(order));
        Assert.That(exception.Message, Is.EqualTo("Quantity and unit price must be non-negative."));
    }

    [Test]
    public void CalculateTotalPrice_NoDiscounts_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new Item("Laptop", 1, 1000.00m),
            new Item("Mouse", 2, 25.00m),
            new Item("Keyboard", 1, 50.00m)
        };
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(1100.00m));
    }

    [Test]
    public void CalculateTotalPrice_QuantityDiscountForOneItem_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new Item("Laptop", 1, 1000.00m),
            new Item("Mouse", 3, 25.00m), // 10% discount
            new Item("Keyboard", 2, 50.00m)
        };
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(1109.12m)); // 1167.50 - 5% = 1109.12
    }

    [Test]
    public void CalculateTotalPrice_QuantityDiscountForAllItems_CalculatesCorrectTotal()
    {
        var order = new List<Item>
        {
            new Item("Laptop", 3, 1000.00m), // 10% discount
            new Item("Mouse", 3, 25.00m),   // 10% discount
            new Item("Keyboard", 3, 50.00m)  // 10% discount
        };
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(2992.50m)); // 3150.00 - 5% = 2992.50
    }

    [Test]
    public void CalculateTotalPrice_TotalExactly100_NoOrderDiscount()
    {
        var order = new List<Item>
        {
            new Item("Laptop", 1, 100.00m)
        };
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(100.00m));
    }

    [Test]
    public void CalculateTotalPrice_TotalAbove100_AppliesOrderDiscount()
    {
        var order = new List<Item>
        {
            new Item("Laptop", 1, 101.00m)
        };
        var totalPrice = _sut.CalculateTotalPrice(order);
        Assert.That(totalPrice, Is.EqualTo(95.95m)); // 101.00 - 5% = 95.95
    }
}