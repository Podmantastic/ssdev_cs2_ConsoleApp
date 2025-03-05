using System;

namespace Ssdev_Cs2_ConsoleApp.Discounts;

public interface IDiscountOptions
{
    /// <summary>
    /// Gets or sets the discount percentage applied to individual items when purchased in bulk quantities.
    /// Value should be between 0 and 1, representing a percentage (e.g., 0.1 for 10%).
    /// </summary>
    decimal BulkItemDiscount { get; set; }
    
    /// <summary>
    /// Gets or sets the discount percentage applied to the entire order when the total exceeds a threshold.
    /// Value should be between 0 and 1, representing a percentage (e.g., 0.05 for 5%).
    /// </summary>
    decimal BulkOrderDiscount { get; set; }
}
