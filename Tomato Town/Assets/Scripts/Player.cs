using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update() {
        UpdateObserver();
    }

    private void FixedUpdate() {
        FixedUpdateObserver();
    }

    public override void UpdateObserver() {
        CheckInputs();
    }

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
        /*
        m_moveInput = PlayerInput.moveInput;
        m_attack = PlayerInput.confirm.isPressed;
        m_jumpHold = PlayerInput.jump.isHeld;
        m_jumpPress = PlayerInput.jump.isPressed;
        */
    }

    #endregion

}
