using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int amount;
}
public class Inventory : MonoBehaviour
{
    public InventoryItem[] items;

}
