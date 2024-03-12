using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private GameControl gameControl;
    private void Awake()
    {
        gameControl = new GameControl();
        gameControl.Player.Enable();
        gameControl.Player.Interact.performed += Interact_Performed;
    }
    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();

        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y).normalized;
        return direction;
    }
}
