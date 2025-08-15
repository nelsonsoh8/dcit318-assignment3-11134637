using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new List<T>();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_log);
        }

        public void SaveToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_log, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("No existing data file found.");
                    return;
                }

                string json = File.ReadAllText(_filePath);
                var items = JsonSerializer.Deserialize<List<T>>(json);

                _log.Clear();
                if (items != null)
                {
                    _log.AddRange(items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
    }
}