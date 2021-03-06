using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAgent : PhysicsAgent
{
    protected IAgentState state { get; private set; }
    protected IAgentState prevState { get; private set; }

    public Hitbox hurtbox { get; private set; }
    public Animator _animator { get; private set; }

    public IAgentState defaultState;
    public bool grounded => isGrounded;
    [HideInInspector] public int maxHP, curHP;
    [HideInInspector] public Vector2 originalScale;


    public override void UpdateComponent() { state.UpdateState(); }
    public override void FixedUpdateComponent() { state.FixedUpdateState(); }
    public void SetState(IAgentState state) { 
        this.prevState = this.state; 
        this.state = state;
        
        if(prevState != null) prevState.ExitState();
        this.state.InitializeState();
    }
    public void RevertState() { SetState(defaultState); }

    protected void Start() {
        _animator = GetComponent<Animator>();
        if(!_animator) _animator = GetComponentInChildren<Animator>();

        originalScale = transform.localScale;
        originalScale.x = Mathf.Abs(originalScale.x);
        hurtbox = new Hitbox(box, originalScale);
    }

    protected void InitializeHealth(int startHP) {
        maxHP = startHP;
        curHP = startHP;
    }

    public virtual void Attacked(Vector2 knockback, int damage = 1) {
        // print("ouch");
        curHP = Mathf.Max(0, curHP - damage);

    }

    // Make agent face a given direction
    public void FaceDirection(float direction) {
        direction = Mathf.Sign(direction);
        transform.localScale = new Vector2(direction * originalScale.x, originalScale.y);
    }

}
