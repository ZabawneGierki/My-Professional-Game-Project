using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Scriptable Objects/ItemSpellEffects/Heal")]
public class Heal : ItemSpellEffect
{
    public override void Cast()
    {
        Debug.Log("the player drunk the healing potion");
    }
}
