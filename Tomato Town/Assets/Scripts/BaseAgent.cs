using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAgent : PhysicsAgent
{
    protected IAgentState state { get; private set; }
    protected Animator _animator;
    protected Vector2 originalScale;

    protected void Start() {
        _animator = GetComponent<Animator>();
        if(!_animator) _animator = GetComponentInChildren<Animator>();

        originalScale = transform.localScale;
        originalScale.x = Mathf.Abs(originalScale.x);
    }

    public void SetState(IAgentState state) {
        this.state = state;
    }

    public override void FixedUpdateComponent() {
        state.FixedUpdateState();
    }

    public override void UpdateComponent() {
        state.UpdateState();
    }

    // Make agent face a given direction
    public void FaceDirection(int direction) {
        transform.localScale = new Vector2(direction * originalScale.x, originalScale.y);
    }

}
