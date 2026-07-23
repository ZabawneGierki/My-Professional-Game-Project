using UnityEngine;

[CreateAssetMenu(fileName = "Cotton", menuName = "Scriptable Objects/ItemSpellEffects/Floating")]
public class Floating : ItemSpellEffect
{
    public override void Cast()
    {
        Debug.Log("The player can float");
        
    }
}
