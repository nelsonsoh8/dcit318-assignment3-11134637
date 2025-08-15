using System;
using WarehouseInventory.Exceptions;
using WarehouseInventory.Models;
using WarehouseInventory.Repositories;

namespace WarehouseInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new WarehouseManager();

            // Seed initial data
            manager.SeedData();

            // Print all items using the public properties
            manager.PrintAllItems(manager.Groceries);
            manager.PrintAllItems(manager.Electronics);

            // Test error cases
            Console.WriteLine("\nTesting error scenarios:");

            try
            {
                // Add duplicate item
                Console.WriteLine("\nAttempting to add duplicate electronic item...");
                manager.Electronics.AddItem(new ElectronicItem(1, "Duplicate Laptop", 5, "HP", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate item error: {ex.Message}");
            }

            try
            {
                // Remove non-existent item
                Console.WriteLine("\nAttempting to remove non-existent grocery item...");
                manager.RemoveItemById(manager.Groceries, 999);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            try
            {
                // Update with invalid quantity
                Console.WriteLine("\nAttempting to update with negative quantity...");
                manager.Groceries.UpdateQuantity(101, -5);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid quantity error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}