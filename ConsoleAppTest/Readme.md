# Order Processing System

This project is an order processing system designed to calculate the total price of customer orders after applying discounts. The system handles orders containing multiple items, each with a price, and applies discounts based on specific rules.

---

## Features

### 1. **Order Structure**
- An order consists of multiple items.
- Each item has:
  - **Name**: The name of the item.
  - **Quantity**: The number of units ordered.
  - **Unit Price**: The price of one unit of the item.

### 2. **Discount Rules**
- **Item-Level Discount**:
  - If the customer buys 3 or more units of the same item, they get a **10% discount** on that item.
- **Order-Level Discount**:
  - If the total price of the order (before discounts) exceeds **$100**, apply a **5% discount** on the entire order.

### 3. **Output**
- The system calculates and returns the total price of the order after applying all applicable discounts.

---

## Requirements

### Functional Requirements
1. Process orders containing multiple items.
2. Apply item-level discounts if the quantity of an item is 3 or more.
3. Apply order-level discounts if the total price before discounts exceeds $100.
4. Return the final total price after applying all discounts.

### Non-Functional Requirements
1. **Scalability**: Handle a large number of items in an order efficiently.
2. **Accuracy**: Ensure discounts are applied correctly based on the rules.
3. **Performance**: Process orders quickly, even with many items.

---

## Design Considerations

### Data Structures
- **Order**: A collection of items.
- **Item**: Represents a single item with properties like name, quantity, and unit price.

### Algorithms
- **Discount Calculation**:
  - Iterate through items to apply item-level discounts.
  - Calculate the total price before discounts to determine if the order-level discount applies.

### Functionality
- **Input**: An order (list of items).
- **Output**: The total price after applying discounts.

---

## Example Use Cases

1. **Item-Level Discount**:
   - Item: "Widget", Quantity: 5, Unit Price: $10.
   - Discount: 10% off on "Widget".
   - Calculation: 5 * $10 = $50 → 10% discount = $5 → Final price for "Widget" = $45.

2. **Order-Level Discount**:
   - Order Total (before discounts): $120.
   - Discount: 5% off on the entire order.
   - Calculation: $120 → 5% discount = $6 → Final order price = $114.

3. **Combined Discounts**:
   - Item 1: "Gadget", Quantity: 4, Unit Price: $20.
   - Item 2: "Tool", Quantity: 2, Unit Price: $30.
   - Item-Level Discount: 10% off on "Gadget".
   - Order Total (before discounts): (4 * $20) + (2 * $30) = $80 + $60 = $140.
   - Order-Level Discount: 5% off on $140.
   - Calculation:
     - "Gadget": 4 * $20 = $80 → 10% discount = $8 → Final price for "Gadget" = $72.
     - "Tool": 2 * $30 = $60 → No discount.
     - Order Total (after item-level discounts): $72 + $60 = $132.
     - Order-Level Discount: 5% off on $132 = $6.60 → Final order price = $125.40.

---

## Getting Started

### Prerequisites
- **.NET SDK**: Ensure you have the .NET SDK installed on your machine.
- **IDE**: Recommended to use Visual Studio 2022 or Visual Studio Code with C# extensions.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/podmantastic/order-processing-system.git