using UnityEngine;

[CreateAssetMenu(fileName = "ManaUp", menuName = "Scriptable Objects/ItemSpellEffects/ManaUp")]
public class ManaUp : ItemSpellEffect
{
    public override void Cast()
    {
        Debug.Log("The player will drink a mana potion");
    }
}
