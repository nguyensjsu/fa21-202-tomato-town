using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMinionState : IAgentState
{
    Minion minion;
    MinionData data => minion.data;

    public IdleMinionState(Minion m) {
        minion = m;
    }

    public void InitializeState() {
        minion.StickToGround();
        minion._animator.SetBool("isIdle", true);
    }
    public void ExitState() {
        minion._animator.SetBool("isIdle", false);
    }

    public void UpdateState() { }

    public void FixedUpdateState() { }

}
