using UnityEngine;
using UnityEngine.UI;

public class SelectorButton : MonoBehaviour
{
    [SerializeField] Button QuickAccessSelector;
     

    public void OnClick()
    {
        // just select the button.
        QuickAccessSelector.Select();
    }
}
