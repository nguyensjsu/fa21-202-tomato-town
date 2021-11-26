using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOFlyState : IAgentState
{
    FlyGuy user;
    FlyGuyData data => user.data;
    bool landed = false;

    public KOFlyState(FlyGuy e) {
        this.user = e;
    }

    public void InitializeState() {
        landed = false;
        user._animator.SetTrigger("ko");
    }

    public void UpdateState() {
        if(landed) return;
        landed = user.grounded;

        if(!landed) return;
        GameManager.gameInstance.RemoveEnemy(user);
        user._animator.SetBool("isGrounded", landed);      
    }

    public void FixedUpdateState() {
        user.ApplyGravity(data.gravity);
    }

    public void ExitState() { }
}
