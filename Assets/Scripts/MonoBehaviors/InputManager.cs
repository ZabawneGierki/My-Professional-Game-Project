using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] private InputSystemUIInputModule uiInputModule;

    private InputActionReference navigationAction;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (uiInputModule == null && EventSystem.current != null)
        {
            EventSystem.current.TryGetComponent(out uiInputModule);
        }

        if (uiInputModule == null)
        {
            Debug.LogError(
                "An InputSystemUIInputModule must be assigned.",
                this);
            return;
        }

        navigationAction = uiInputModule.move;
    }

    public void EnablePlayerActionMap()
    {
        inputActions.FindActionMap(ActionMaps.Player.PlayerActionMap).Enable();
        inputActions.FindActionMap(ActionMaps.UI.UIActionMap).Disable();
    }

    public void EnableUIActionMap()
    {
        inputActions.FindActionMap(ActionMaps.UI.UIActionMap).Enable();
        inputActions.FindActionMap(ActionMaps.Player.PlayerActionMap).Disable();
    }

    [ContextMenu("Disable navigation")]
    public void DisableUINav()
    {
        if (uiInputModule != null)
        {
            uiInputModule.move = null;
        }
    }

    [ContextMenu("Enable navigation")]
    public void EnableUINav()
    {
        if (uiInputModule != null)
        {
            uiInputModule.move = navigationAction;
        }
    }
}
