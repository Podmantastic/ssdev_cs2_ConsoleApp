namespace Ssdev_Cs2_ConsoleApp;

/// <summary>
/// Represents an item with a name, quantity, and unit price.
/// </summary>
/// <param name="Name">The name of the item.</param>
/// <param name="Quantity">The quantity of the item.</param>
/// <param name="UnitPrice">The unit price of the item.</param>
public record Item(string Name, int Quantity, decimal UnitPrice);
