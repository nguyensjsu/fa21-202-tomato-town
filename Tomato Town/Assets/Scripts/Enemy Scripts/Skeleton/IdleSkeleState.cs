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

    public void UpdateState() {
        // Check if player is inside strike radius and swing if so
        if(user.SearchPlayer(data.strikeRadius)) {
            user.attackState.SetAttack(data.basic);
            user.SetState(user.attackState);
            user.FacePlayer();
        }
    }

    public void FixedUpdateState() {
        user.ApplyGravity(data.gravity);
    }

    public void ExitState() { }
}
