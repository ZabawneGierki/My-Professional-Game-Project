using UnityEngine;
using UnityEngine.InputSystem;

public class QuickAccessMenu : MonoBehaviour
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }
    public ItemSpellEffect up, down, right, left; // assign starting minispells that can be changed in the future

    [SerializeField] private InputActionReference quickAccessMenuInputAction; // assign the input action for opening the quick access menu

    public void AssignMiniSpell(Directions direction, ItemSpellEffect miniSpell)
    {
        switch (direction)
        {
            case Directions.Up:
                up = miniSpell;
                break;
            case Directions.Down:
                down = miniSpell;
                break;
            case Directions.Left:
                left = miniSpell;
                break;
            case Directions.Right:
                right = miniSpell;
                break;
        }
    }



    private void OnEnable()
    {
        quickAccessMenuInputAction.action.performed += OnQuickAccessMenuPerformed;
    }



    private void OnDisable()
    {
        quickAccessMenuInputAction.action.performed -= OnQuickAccessMenuPerformed;
    }


    private void OnQuickAccessMenuPerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        // Handle the direction input
        
        if (direction.y == 1f)
        {
            up.Cast();
        }
        else if (direction.y == 1f)
        {
            down.Cast();
        }
        else if (direction.x == 1f)
        {
            left.Cast();
        }
        else if (direction.x == 1f)
        {
            right.Cast();
        }

    }

     
}