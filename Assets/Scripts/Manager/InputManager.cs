using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] float minimumInputThreshold = 0.1f;
    private InputSystem_Actions playerInput;
    public event EventHandler OnAttackPerformed;
    public event EventHandler OnAttackReleased;
    
    public enum InputActions
    {
        NONE,
        MOVE,
        ATTACK,
        INTERACT,
        DASH,
        ABILITY
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of InputManager detected.");
        }

        Instance = this;

        playerInput = new InputSystem_Actions();
        playerInput.Enable();
        
        playerInput.Player.Attack.started += PlayerAttackStarted;
        playerInput.Player.Attack.canceled += PlayerAttackReleased;
    }

    private void PlayerAttackReleased(InputAction.CallbackContext obj)
    {
        OnAttackReleased?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttackStarted(InputAction.CallbackContext obj)
    {
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementDirection()
    {
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();
        
        if (input.magnitude < minimumInputThreshold)
        {
            return Vector2.zero;
        }
        
        return playerInput.Player.Move.ReadValue<Vector2>();
    }

}
