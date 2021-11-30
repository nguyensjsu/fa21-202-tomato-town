using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : BaseAgent
{
    public PlayerData data;

    private bool INVINCIBLE 
        => state == hurtState 
        || state == koState;

    public IAgentState hurtState, koState;
    public PlayerAttackState attackState;

    private IAgentState minionSubState;
    public IAgentState haveMinionState, noMinionState;

    [HideInInspector] public bool hasMinion => minionSubState == haveMinionState;
    [HideInInspector] public Minion minion;

    public int coins { get; private set; }

    // Start is called before the first frame update
    new void Start() {
        base.Start();

        defaultState = new DefaultPlayerState(this);
        hurtState = new PlayerHurtState(this);
        koState = new PlayerKOState(this);
        attackState = new PlayerAttackState(this,data.basic);
        SetState(defaultState);

        noMinionState = new NoMinionState(this);
        haveMinionState = new HaveMinionState(this);
        SetSubState(noMinionState);
        InitializeHealth(data.hp);

        curHP = GameData.playerHP;
        coins = GameData.playerCoins;
        PlayerHUD.instance.UpdateHP(curHP);
    }

    public override void UpdateComponent() {
        base.UpdateComponent();
        CheckInputs();
        minionSubState.UpdateState();
    }

    public override void FixedUpdateComponent() {
        base.FixedUpdateComponent();
        minionSubState.FixedUpdateState();
    }

    public void SetSubState(IAgentState s) {
        minionSubState = s;
    }

    public override void Attacked(Vector2 knockback, int damage = 1) {
        if(INVINCIBLE) return;

        base.Attacked(knockback,damage);

        velocity = data.hurtKnockback;
        velocity.x *= Mathf.Sign(knockback.x);
        if(curHP <= 0) SetState(koState);
        else SetState(hurtState);
        PlayerHUD.instance.UpdateHP(curHP);
        SoundManager.instance.PlayHurt();
    }

    public void AdjustHealth(int value) {
        curHP = Mathf.Clamp(curHP + value, 0, maxHP);
        PlayerHUD.instance.UpdateHP(curHP);
    }

    public void AdjustCoinAmount(int value) {
        coins = Mathf.Max(0,coins + value);
        PlayerHUD.instance.UpdateCoins(coins);
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
    private bool endFloat;
    protected float jumpChance;
    private Timer jumpTimer = new Timer();
    protected float coyoteTime => Time.time - jumpChance;

    // Initiate jump
    public void Jump(float jumpHeight,float airSpeed,bool resetFloat = true) {
        SoundManager.instance.PlayJump();

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
    public bool m_item { get; private set; }
    public bool m_jumpPress { get; private set; }
    public bool m_jumpHold { get; private set; }
    public bool m_jumpRelease { get; private set; }
    public bool m_isMoving => Mathf.Abs(m_moveInput.x) > 0f;
    public bool m_upDirection => m_moveInput.y > 0.4f;
    public bool m_downDirection => m_moveInput.y <= -0.6f;

    private void CheckInputs() {
        m_moveInput = PlayerInput.moveInput;
        m_attack = PlayerInput.confirm.isPressed;
        m_item = PlayerInput.cancel.isPressed;
        m_jumpHold = PlayerInput.jump.isHeld;
        m_jumpPress = PlayerInput.jump.isPressed;
    }

    #endregion

}
