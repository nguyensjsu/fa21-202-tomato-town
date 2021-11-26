using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleFlyState : IAgentState
{
    FlyGuy user;
    FlyGuyData data => user.data;

    public IdleFlyState(FlyGuy e) {
        this.user = e;
    }

    public void InitializeState() { }

    public void UpdateState() {
        user.MoveForward(data.walkSpeed);
    }

    public void FixedUpdateState() { }

    public void ExitState() { }
}
