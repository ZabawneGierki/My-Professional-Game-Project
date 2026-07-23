using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject MenuGameObject;
    private InputAction selectAction;
    private bool isMenuActive = false;


    private void OnEnable()
    {
        MenuGameObject.SetActive(false); // make sure it is inactive at the start.
        selectAction = InputManager.Instance.inputActions.FindAction(ActionMaps.UI.Select, true);
        selectAction.performed += OnSelect;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {

        isMenuActive = !isMenuActive;

        MenuGameObject.SetActive(isMenuActive);

        if (isMenuActive)
            Time.timeScale = 0.0f;

        else
            Time.timeScale = 1.0f;
    }

    private void OnDisable()
    {
        if (selectAction == null)
        {
            Debug.LogError("select action map is not assigned!"); return;
        }
        selectAction.performed -= OnSelect;

    }
}
