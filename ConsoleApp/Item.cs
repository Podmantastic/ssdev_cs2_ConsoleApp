namespace ConsoleApp;

public class Item
{
    public string Name { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public Item(string name, int quantity, decimal unitPrice)
    {
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
