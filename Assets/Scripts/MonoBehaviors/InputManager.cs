using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

    }

    public void EnablePlayerActionMap()
    {
        inputActions.FindActionMap(ActionMaps.Player.PlayerActionMap).Enable();
        // also disable UI action map
        inputActions.FindActionMap(ActionMaps.UI.UIActionMap).Disable();
    }

    public void EnableUIActionMap()
    {
        inputActions.FindActionMap(ActionMaps.UI.UIActionMap).Enable();
        // also disable Player action map
        inputActions.FindActionMap(ActionMaps.Player.PlayerActionMap).Disable();
    }


}
