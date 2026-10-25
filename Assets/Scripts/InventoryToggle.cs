
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private KeyCode toggleKey = KeyCode.I;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
