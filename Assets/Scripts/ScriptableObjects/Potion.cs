using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : Item, IUsable
{
    public void Use()
    {
        // Implement potion usage logic here
    }
}
