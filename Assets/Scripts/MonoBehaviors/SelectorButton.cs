using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectorButton : MonoBehaviour
{
    [SerializeField] Button QuickAccessSelector;
     

    public void OnClick()
    {
        QuickAccessSelector.Select();
    }



    
}
