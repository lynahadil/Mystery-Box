
using System.Text;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private TMP_Text inventoryText;

    void OnEnable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged += RefreshUI;
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= RefreshUI;
    }

    void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        if (inventory == null || inventoryText == null)
            return;

        StringBuilder text = new StringBuilder();
        text.AppendLine("INVENTORY");

        if (inventory.Items.Count == 0)
        {
            text.AppendLine("Empty");
        }
        else
        {
            foreach (ItemData item in inventory.Items)
                text.AppendLine(item.ItemName);
        }

        inventoryText.text = text.ToString();
    }
}
