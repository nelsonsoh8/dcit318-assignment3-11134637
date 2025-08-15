using System;
using InventorySystem.Models;
using InventorySystem.Repositories;

namespace InventorySystem
{
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;

        public InventoryApp()
        {
            _logger = new InventoryLogger<InventoryItem>("inventory.json");
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now.AddDays(-5)));
            _logger.Add(new InventoryItem(2, "Monitor", 15, DateTime.Now.AddDays(-3)));
            _logger.Add(new InventoryItem(3, "Keyboard", 25, DateTime.Now.AddDays(-1)));
            _logger.Add(new InventoryItem(4, "Mouse", 30, DateTime.Now));
            _logger.Add(new InventoryItem(5, "Headphones", 12, DateTime.Now));

            Console.WriteLine("Sample data seeded successfully.");
        }

        public void SaveData()
        {
            _logger.SaveToFile();
            Console.WriteLine("Data saved to file.");
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
            Console.WriteLine("Data loaded from file.");
        }

        public void PrintAllItems()
        {
            Console.WriteLine("\nCurrent Inventory:");
            foreach (var item in _logger.GetAll())
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Added: {item.DateAdded:yyyy-MM-dd}");
            }
        }
    }
}