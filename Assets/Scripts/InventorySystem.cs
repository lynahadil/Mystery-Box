using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public event Action OnInventoryChanged;

    public List<ItemData> Items { get; private set; }
        = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (item == null)
            return;

        Items.Add(item);
        OnInventoryChanged?.Invoke();

        Debug.Log("Added to inventory: " + item.ItemName);
    }

    public int GetItemCount()
    {
        return Items.Count;
    }

    public List<ItemData> GetItems()
    {
        return new List<ItemData>(Items);
    }
}