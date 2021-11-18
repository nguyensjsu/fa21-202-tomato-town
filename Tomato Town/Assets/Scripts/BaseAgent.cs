using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShaderManager))]
public abstract class BaseAgent : PhysicsAgent
{
    protected IAgentState state { get; private set; }
    protected IAgentState prevState { get; private set; }
    
    [HideInInspector] public Vector2 originalScale;
    public Hitbox hurtbox { get; private set; }
    public Animator _animator { get; private set; }
    public bool grounded => isGrounded;
    public ShaderManager shader { get; private set; }


    public override void UpdateComponent() { state.UpdateState(); }
    public override void FixedUpdateComponent() { state.FixedUpdateState(); }
    public void SetState(IAgentState state) { 
        this.prevState = this.state; 
        this.state = state;
        
        if(prevState != null) prevState.ExitState();
        this.state.InitializeState();
    }
    public void RevertState() { SetState(this.prevState); }


    protected void Start() {
        _animator = GetComponent<Animator>();
        if(!_animator) _animator = GetComponentInChildren<Animator>();

        originalScale = transform.localScale;
        originalScale.x = Mathf.Abs(originalScale.x);
        hurtbox = new Hitbox(box, originalScale);
        shader = GetComponent<ShaderManager>();
    }

    public virtual void Attacked(Vector2 knockback) {
        print("ouch");
    }

    // Make agent face a given direction
    public void FaceDirection(float direction) {
        direction = Mathf.Sign(direction);
        transform.localScale = new Vector2(direction * originalScale.x, originalScale.y);
    }

}
