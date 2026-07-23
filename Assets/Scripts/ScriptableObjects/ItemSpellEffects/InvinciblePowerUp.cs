using UnityEngine;

[CreateAssetMenu(fileName = "InvinciblePowerUp", menuName = "Scriptable Objects/ItemSpellEffects/InvinciblePowerUp")]
public class InvinciblePowerUp : ItemSpellEffect
{ 
    public override void Cast()
    {
        Debug.Log("I am invincible");
    }
}
