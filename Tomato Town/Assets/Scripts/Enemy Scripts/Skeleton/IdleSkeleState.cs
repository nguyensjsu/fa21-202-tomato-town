using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSkeleState : IAgentState
{
    Skeleton user;
    SkeletonData data => user.data;

    public IdleSkeleState(Skeleton e) {
        this.user = e;
    }

    public void InitializeState() { }

    public void UpdateState() { }

    public void FixedUpdateState() {
        user.ApplyGravity(data.gravity);
    }

    public void ExitState() { }

    private void Swing() {
        user.attackState.SetAttack(data.basic);
        user.SetState(user.attackState);
    }
}
