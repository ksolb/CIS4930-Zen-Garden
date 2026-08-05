using UnityEngine;
using UnityEngine.InputSystem;

public class MenuButtonListener : MonoBehaviour
{
    [SerializeField] private InputActionReference menuButtonAction;
    [SerializeField] private VRMenuFollow menu;

    private void OnEnable()
    {
        menuButtonAction.action.performed += OnMenuPressed;
    }

    private void OnDisable()
    {
        menuButtonAction.action.performed -= OnMenuPressed;
    }

    private void OnMenuPressed(InputAction.CallbackContext ctx)
    {
        Debug.Log("Menu Button pressed!!");
        menu.ToggleMenu();
    }
}