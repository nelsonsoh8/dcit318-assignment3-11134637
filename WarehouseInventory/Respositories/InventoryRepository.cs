using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Exceptions;
using WarehouseInventory.Models;

namespace WarehouseInventory.Repositories
{
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new Dictionary<int, T>();

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
            {
                throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
            }
            _items.Add(item.Id, item);
        }

        public T GetItemById(int id)
        {
            if (!_items.TryGetValue(id, out T item))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            }
            return item;
        }

        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found.");
            }
            _items.Remove(id);
        }

        public List<T> GetAllItems() => _items.Values.ToList();

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new InvalidQuantityException("Quantity cannot be negative.");
            }

            var item = GetItemById(id);
            item.Quantity = newQuantity;
        }
    }
}