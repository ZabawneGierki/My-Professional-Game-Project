using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickAccessMenu : MonoBehaviour
{

    [SerializeField] Image FirstImage, SecondImage, ThirdImage, LastImage;

    public Usable first, second, third, last;
    //public ItemSpellEffect up, down, right, left;

   

    private InputAction quickAccessMenuAction;

    private void Awake()
    {

        
    }
    private void AssignImageSprites()
    {
        FirstImage.sprite = first.itemSprite;
        SecondImage.sprite = second.itemSprite;
        ThirdImage.sprite = third.itemSprite;
        LastImage.sprite = last.itemSprite;
    }
    public void AssignMiniSpell(Direction direction, Usable usable)
    {
        switch (direction)
        {
            case Direction.Up:
                first = usable;
                break;
            case Direction.Down:
                third = usable;
                break;
            case Direction.Left:
                last = usable;
                break;
            case Direction.Right:
                second = usable;
                break;
        }

        AssignImageSprites();
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

        AssignImageSprites();

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
                 
                first.Effect.Cast();
            }
            else if (direction.y < 0f)
            {
                third.Effect.Cast();
            }
        }
        else if (direction.x < 0f)
        {
            last.Effect.Cast();
        }
        else if (direction.x > 0f)
        {
            second.Effect.Cast();
        }
    }
}