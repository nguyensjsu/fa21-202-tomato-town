using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOSkeleState : IAgentState
{
    Skeleton user;
    SkeletonData data => user.data;

    public KOSkeleState(Skeleton e) {
        this.user = e;
    }

    public void InitializeState() {
        user._animator.SetTrigger("ko");
        GameManager.gameInstance.RemoveEnemy(user);
    }

    public void UpdateState() { }

    public void FixedUpdateState() {
        user.ApplyGravity(data.gravity);
    }

    public void ExitState() { }
}
