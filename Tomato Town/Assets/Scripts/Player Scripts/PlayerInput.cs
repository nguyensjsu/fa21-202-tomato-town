using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // Public variables to check input
    public static bool isMoving => moveInput.x != 0;
    public static Vector2 moveInput => movement.ReadValue<Vector2>();
    public static ButtonInput jump;
    public static ButtonInput confirm;
    public static ButtonInput cancel;

    // Main input functions
    private PlayerInputActions inputActions;
    private static InputAction movement;

    public struct ButtonInput
    {
        private InputAction input;
        public ButtonInput(InputAction a) {
            input = a;
            isHeld = false;
            releasedFrame = -1;
        }

        public int releasedFrame;

        public bool isHeld;
        public bool isPressed => input.triggered;
        public bool isReleased => Time.frameCount == releasedFrame;
    }

    void Awake() {
        inputActions = new PlayerInputActions();
        movement = inputActions.Player.Movement;

        jump = new ButtonInput(inputActions.Player.Jump);
        inputActions.Player.Jump.started += ctx => jump.isHeld = true;
        inputActions.Player.Jump.canceled += ctx => {
            jump.isHeld = false;
            jump.releasedFrame = Time.frameCount;
        };

        confirm = new ButtonInput(inputActions.Player.Confirm);
        inputActions.Player.Confirm.started += ctx => confirm.isHeld = true;
        inputActions.Player.Confirm.canceled += ctx => {
            confirm.isHeld = false;
            confirm.releasedFrame = Time.frameCount;
        };

        cancel = new ButtonInput(inputActions.Player.Cancel);
        inputActions.Player.Cancel.started += ctx => cancel.isHeld = true;
        inputActions.Player.Cancel.canceled += ctx => {
            cancel.isHeld = false;
            cancel.releasedFrame = Time.frameCount;
        };
    }

    private void OnEnable() { inputActions.Enable(); }
    private void OnDisable() { inputActions.Disable(); }
}