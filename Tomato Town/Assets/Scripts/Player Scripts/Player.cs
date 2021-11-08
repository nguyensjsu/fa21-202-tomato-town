using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : BaseAgent
{
    public bool grounded => isGrounded;
    public PlayerData data;

    public IAgentState defaultState;
    public AttackBaseState attackState;

    public bool hasBooster;
    
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        GameManager.gameInstance.AddChild(this);

        defaultState = new DefaultPlayerState(this);
        attackState = new PlayerAttackState(this,data.basic);
        SetState(defaultState);
    }

    public override void UpdateComponent() {
        base.UpdateComponent();
        CheckInputs();
    }

    #region Movement Functions

    public void HorizontalMovement(float speed,bool flip) {
        if(!m_isMoving) return;
        var dir = (int)Mathf.Sign(m_moveInput.x);
        if(flip) FaceDirection(dir);
        var dir2 = m_moveInput.x;
        Move(Vector2.right * (dir2 * speed * Time.deltaTime));
    }

    // Handles the player's air movement
    public bool endFloat;
    protected float jumpChance;
    private Timer jumpTimer = new Timer();
    protected float coyoteTime => Time.time - jumpChance;

    // Initiate jump
    public void Jump(float jumpHeight,float airSpeed,bool resetFloat = true) {
        jumpChance = 0;
        isGrounded = false;
        endFloat = !resetFloat;
        jumpTimer.ResetTimer();
        velocity.y = Mathf.Abs(jumpHeight);
        if(Mathf.Abs(velocity.x) < Mathf.Epsilon) return;
        velocity.x = Mathf.Sign(velocity.x) * airSpeed;
    }

    // Determines how long the player can continue rising in their jump
    public void HandleJumpFloat() {
        if(endFloat) return;

        if(m_jumpHold) velocity.y = Mathf.Max(velocity.y, Mathf.Abs(data.jumpForce));
        endFloat = !m_jumpHold || jumpTimer.WaitForXFrames(data.jumpHold);
        if(endFloat || (!m_jumpHold && velocity.y > 0)) { velocity.y *= 0.5f; }
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
