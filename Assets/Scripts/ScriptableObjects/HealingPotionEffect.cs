using UnityEngine;

[CreateAssetMenu(fileName = "HealingPotion", menuName = "Scriptable Objects/HealingPotion")]
public class HealingPotionEffect : ItemSpellEffect
{

    public Item associatedItem; // The item associated with this spell effect
   

    [TextArea]
    [SerializeField] private string spellText;



    private int amountOfItems;
    private Inventory playerInventory;


    public void Initialize(Inventory inventory)
    {
        playerInventory = inventory;
        // Find the associated item in the player's inventory and get its amount
        foreach (var inventoryItem in playerInventory.items)
        {
            if (inventoryItem.item == associatedItem)
            {
                amountOfItems = inventoryItem.amount;
                break;
            }
        }
    }
    public override void Cast()
    {
        if (amountOfItems <= 0)
        {
            Debug.Log("No more healing potions left!");
            return;
        }
        Debug.Log(spellText);
        amountOfItems--;
        // Implement healing logic here
    }
}
