using UnityEngine;
using UnityEngine.UI;

public class KeybindsTab : MonoBehaviour
{
    readonly Button firstSelectedButton; // we will select the top left button as the first

    private void OnEnable()
    {
        firstSelectedButton.Select();
        
    }
}
