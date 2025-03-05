namespace Ssdev_Cs2_ConsoleApp.Discounts;

/// <summary>
/// Default implementation of the IDiscountOptions interface.
/// </summary>
public class DiscountOptions : IDiscountOptions
{
    /// <summary>
    /// Gets or sets the discount percentage applied to individual items when purchased in bulk quantities.
    /// Default value is 10% (0.10m).
    /// </summary>
    public decimal BulkItemDiscount { get; set; } = 0.10m;
    
    /// <summary>
    /// Gets or sets the discount percentage applied to the entire order when the total exceeds a threshold.
    /// Default value is 5% (0.05m).
    /// </summary>
    public decimal BulkOrderDiscount { get; set; } = 0.05m;
}