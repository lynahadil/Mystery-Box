
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Mystery Box/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private float rewardDisplayScale = 0.5f;
    [Header("Item Information")]
    [SerializeField] private string itemName;
    [SerializeField] private int value;
    [SerializeField] private ItemRarity rarity;

    [Header("Drop Settings")]
    [SerializeField, Min(0f)] private float dropWeight = 1f;

    [Header("3D Reward Model")]
    [SerializeField] private GameObject itemPrefab;

    public string ItemName => itemName;
    public int Value => value;
    public ItemRarity Rarity => rarity;
    public float DropWeight => dropWeight;
    public GameObject ItemPrefab => itemPrefab;
}

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
