using UnityEngine;
using UnityEngine.InputSystem;

public class QuickAccessMenu : MonoBehaviour
{
    public ItemSpellEffect up, down, right, left;

   

    private InputAction quickAccessMenuAction;

    public void AssignMiniSpell(Direction direction, ItemSpellEffect miniSpell)
    {
        switch (direction)
        {
            case Direction.Up:
                up = miniSpell;
                break;
            case Direction.Down:
                down = miniSpell;
                break;
            case Direction.Left:
                left = miniSpell;
                break;
            case Direction.Right:
                right = miniSpell;
                break;
        }
    }

    private void Start()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("An InputManager instance is required.", this);
            return;
        }

        quickAccessMenuAction = InputManager.Instance.inputActions.FindAction(
            ActionMaps.UI.QuickAccess,
            true);

        SubscribeToInput();
    }

    private void OnEnable()
    {
        SubscribeToInput();
    }

    private void OnDisable()
    {
        if (quickAccessMenuAction != null)
        {
            quickAccessMenuAction.performed -= OnQuickAccessMenuPerformed;
        }
    }

    private void SubscribeToInput()
    {
        if (quickAccessMenuAction == null)
        {
            return;
        }

        quickAccessMenuAction.performed -= OnQuickAccessMenuPerformed;
        quickAccessMenuAction.performed += OnQuickAccessMenuPerformed;
    }

    private void OnQuickAccessMenuPerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
        {
            if (direction.y > 0f)
            {
                up?.Cast();
            }
            else if (direction.y < 0f)
            {
                down?.Cast();
            }
        }
        else if (direction.x < 0f)
        {
            left?.Cast();
        }
        else if (direction.x > 0f)
        {
            right?.Cast();
        }
    }
}