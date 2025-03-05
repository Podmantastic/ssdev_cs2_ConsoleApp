using NUnit.Framework;
using Moq;
using Ssdev_Cs2_ConsoleApp.DiscountStrategies;
using Ssdev_Cs2_ConsoleApp;
using Ssdev_Cs2_ConsoleApp.DTO;

[TestFixture]
public class OrderDiscountCalculatorTests
{
    private Mock<IDiscountStrategy> _mockStrategy1 = null!;
    private Mock<IDiscountStrategy> _mockStrategy2 = null!;
    private OrderDiscountCalculator _calculator = null!;
    
    [SetUp]
    public void Setup()
    {
        _mockStrategy1 = new Mock<IDiscountStrategy>();
        _mockStrategy2 = new Mock<IDiscountStrategy>();
        
        _calculator = new OrderDiscountCalculator(new List<IDiscountStrategy> 
        { 
            _mockStrategy1.Object, 
            _mockStrategy2.Object 
        });
    }
    
    [Test]
    public void Do_ValidOrder_ReturnsCombinedDiscountRoundedToTwoDecimalPlaces()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 10.0m),
            new Item("Product B", 2, 20.0m)
        };
        
        _mockStrategy1.Setup(s => s.CalculateDiscount(order)).Returns(3.333m);
        _mockStrategy2.Setup(s => s.CalculateDiscount(order)).Returns(5.667m);
        
        // Act
        var result = _calculator.Do(order);
        
        // Assert
        Assert.That(result, Is.EqualTo(9.00m)); // 3.333 + 5.667 = 9.000, rounded to 9.00
        _mockStrategy1.Verify(s => s.CalculateDiscount(order), Times.Once);
        _mockStrategy2.Verify(s => s.CalculateDiscount(order), Times.Once);
    }
    
    [Test]
    public void Do_NullOrder_ThrowsArgumentNullException()
    {
        // Arrange
        List<Item>? order = null;

        // Act & Assert
#pragma warning disable CS8604 // Possible null reference argument.
        var ex = Assert.Throws<ArgumentNullException>(() => _calculator.Do(order));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.ParamName, Is.EqualTo("order"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.Message, Does.Contain("Order items list cannot be null"));
    }
    
    [Test]
    public void Do_EmptyOrder_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>();
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Do(order));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.ParamName, Is.EqualTo("order"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.Message, Does.Contain("Order items list cannot be empty"));
    }
    
    [Test]
    public void Do_ItemWithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("", 1, 10.0m)
        };
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Do(order));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.Message, Does.Contain("Item name cannot be null or empty"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    
    [Test]
    public void Do_ItemWithNegativeUnitPrice_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 1, -10.0m)
        };
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Do(order));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.Message, Does.Contain("Unit price for Product A cannot be negative"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    
    [Test]
    public void Do_ItemWithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", -1, 10.0m)
        };
        
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _calculator.Do(order));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Assert.That(ex.Message, Does.Contain("Quantity for Product A cannot be negative"));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    
    [Test]
    public void Do_NoDiscountStrategies_ReturnsZero()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 1, 10.0m)
        };
        
        var calculatorWithNoStrategies = new OrderDiscountCalculator(new List<IDiscountStrategy>());
        
        // Act
        var result = calculatorWithNoStrategies.Do(order);
        
        // Assert
        Assert.That(result, Is.EqualTo(0.00m));
    }
    
    [Test]
    public void Do_MultipleDiscountStrategies_SumsAllDiscounts()
    {
        // Arrange
        var order = new List<Item>
        {
            new Item("Product A", 3, 10.0m),
            new Item("Product B", 2, 20.0m)
        };
        
        var mockStrategy1 = new Mock<IDiscountStrategy>();
        var mockStrategy2 = new Mock<IDiscountStrategy>();
        var mockStrategy3 = new Mock<IDiscountStrategy>();
        
        mockStrategy1.Setup(s => s.CalculateDiscount(order)).Returns(1.5m);
        mockStrategy2.Setup(s => s.CalculateDiscount(order)).Returns(2.5m);
        mockStrategy3.Setup(s => s.CalculateDiscount(order)).Returns(3.5m);
        
        var calculator = new OrderDiscountCalculator(new List<IDiscountStrategy> 
        { 
            mockStrategy1.Object, 
            mockStrategy2.Object,
            mockStrategy3.Object
        });
        
        // Act
        var result = calculator.Do(order);
        
        // Assert
        Assert.That(result, Is.EqualTo(7.50m)); // 1.5 + 2.5 + 3.5 = 7.5
        mockStrategy1.Verify(s => s.CalculateDiscount(order), Times.Once);
        mockStrategy2.Verify(s => s.CalculateDiscount(order), Times.Once);
        mockStrategy3.Verify(s => s.CalculateDiscount(order), Times.Once);
    }
}