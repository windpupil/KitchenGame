using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private const string GAMEINPUT_BINDINGS = "GameInputBindings";
    public event EventHandler OnInteractAction;
    public event EventHandler OnOperateAction;
    public event EventHandler OnPauseAction;
    private GameControl gameControl;

    public enum BindingType
    {
        Up,
        Down,
        Left,
        Right,
        Interact,
        Operate,
        Pause
    }
    private void Awake()
    {
        Instance = this;
        gameControl = new GameControl();
        if (PlayerPrefs.HasKey(GAMEINPUT_BINDINGS))
        {
            gameControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString(GAMEINPUT_BINDINGS));
        }
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_Performed;
        gameControl.Player.Pause.performed += Pause_Performed;
    }
    private void OnDestroy()
    {
        gameControl.Player.Interact.performed -= Interact_Performed;
        gameControl.Player.Operate.performed -= Operate_Performed;
        gameControl.Player.Pause.performed -= Pause_Performed;
        gameControl.Dispose();
    }
    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();

        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y).normalized;
        return direction;
    }
    public string GetBindingDisplayString(BindingType bindingType)
    {
        switch (bindingType)
        {
            case BindingType.Up:
                return gameControl.Player.Move.bindings[1].ToDisplayString();
            case BindingType.Down:
                return gameControl.Player.Move.bindings[2].ToDisplayString();
            case BindingType.Left:
                return gameControl.Player.Move.bindings[3].ToDisplayString();
            case BindingType.Right:
                return gameControl.Player.Move.bindings[4].ToDisplayString();
            case BindingType.Interact:
                return gameControl.Player.Interact.bindings[0].ToDisplayString();
            case BindingType.Operate:
                return gameControl.Player.Operate.bindings[0].ToDisplayString();
            case BindingType.Pause:
                return gameControl.Player.Pause.bindings[0].ToDisplayString();
            default:
                break;
        }
        return "";
    }
    public void ReBinding(BindingType bindingType, Action onComlete)
    {
        gameControl.Player.Disable();
        InputAction inputAction = null;
        int index = -1;
        switch (bindingType)
        {
            case BindingType.Up:
                inputAction = gameControl.Player.Move;
                index = 1;
                break;
            case BindingType.Down:
                inputAction = gameControl.Player.Move;
                index = 2;
                break;
            case BindingType.Left:
                inputAction = gameControl.Player.Move;
                index = 3;
                break;
            case BindingType.Right:
                inputAction = gameControl.Player.Move;
                index = 4;
                break;
            case BindingType.Interact:
                inputAction = gameControl.Player.Interact;
                index = 0;
                break;
            case BindingType.Operate:
                inputAction = gameControl.Player.Operate;
                index = 0;
                break;
            case BindingType.Pause:
                inputAction = gameControl.Player.Pause;
                index = 0;
                break;
            default:
                break;
        }
        inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
        {
            callback.Dispose();
            gameControl.Player.Enable();
            onComlete?.Invoke();

            PlayerPrefs.SetString(GAMEINPUT_BINDINGS, gameControl.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}