using UnityEngine;


public abstract class ItemSpellEffect : ScriptableObject
{
    public ItemName associatedItemName;
    


    /// <summary>
    /// A reference to the player Object.
    /// </summary>
    private GameObject playerGameObject;

    /// <summary>
    /// Assigns the player GameObject to the ItemSpellEffect instance. This allows the spell to interact with the player when activated.
    /// </summary>
    /// <param name="player"></param>
    public void Initialize(GameObject player)
    {
        playerGameObject = player;
    }
    public abstract void Cast();
    

}
