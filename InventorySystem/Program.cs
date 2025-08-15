using System;

namespace InventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventory Management System");

            // Create and initialize first instance
            var app = new InventoryApp();

            // Seed, save, and clear
            app.SeedSampleData();
            app.SaveData();

            Console.WriteLine("\nSimulating new session...");

            // Create new instance to simulate new session
            var newApp = new InventoryApp();
            newApp.LoadData();
            newApp.PrintAllItems();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}