using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : BaseAgent
{
    public bool grounded => isGrounded;
    public PlayerData data;

    public IAgentState defaultState;
    
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        defaultState = new DefaultPlayerState(this);
        SetState(defaultState);
    }

    #region REMOVE ME LATER
    private void Update() {
        UpdateObserver();
    }

    private void FixedUpdate() {
        FixedUpdateObserver();
    }
    #endregion

    public override void UpdateObserver() {
        base.UpdateObserver();
        CheckInputs();
    }

    #region Movement Functions

    public void HorizontalMovement(float speed,bool flip) {
        if(!m_isMoving) return;
        var dir = (int)Mathf.Sign(m_moveInput.x);
        if(flip) FaceDirection(dir);
        Move(Vector2.right * (dir * speed * Time.deltaTime));
    }

    // Handles the player's air movement
    protected bool endFloat;
    protected float jumpChance;
    private Timer jumpTimer = new Timer();
    protected float coyoteTime => Time.time - jumpChance;

    // Initiate jump
    public void Jump(float jumpHeight,float airSpeed) {
        jumpChance = 0;
        endFloat = false;
        isGrounded = false;
        jumpTimer.ResetTimer();
        velocity.y = Mathf.Abs(jumpHeight);
        if(Mathf.Abs(velocity.x) < Mathf.Epsilon) return;
        velocity.x = Mathf.Sign(velocity.x) * airSpeed;
    }

    // Determines how long the player can continue rising in their jump
    public void HandleJumpFloat() {
        if(!m_jumpHold && velocity.y > 0)
            velocity.y *= 0.5f;

        if(endFloat) return;
        if(m_jumpHold) velocity.y = Mathf.Abs(data.jumpForce);
        endFloat = !m_jumpHold || jumpTimer.WaitForXFrames(data.jumpHold);
    }

    #endregion

    #region Inputs

    public Vector2 m_moveInput { get; private set; }
    public bool m_attack { get; private set; }
    public bool m_jumpPress { get; private set; }
    public bool m_jumpHold { get; private set; }
    public bool m_jumpRelease { get; private set; }
    public bool m_isMoving => Mathf.Abs(m_moveInput.x) > 0f;
    public bool m_upDirection => m_moveInput.y > 0.4f;
    public bool m_downDirection => m_moveInput.y <= -0.6f;

    private void CheckInputs() {
        m_moveInput = PlayerInput.moveInput;
        m_attack = PlayerInput.confirm.isPressed;
        m_jumpHold = PlayerInput.jump.isHeld;
        m_jumpPress = PlayerInput.jump.isPressed;
    }

    #endregion

}
