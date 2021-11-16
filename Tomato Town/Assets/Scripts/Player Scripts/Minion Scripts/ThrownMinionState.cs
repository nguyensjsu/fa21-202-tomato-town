using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownMinionState : IAgentState
{
    Minion minion;
    MinionData data => minion.data;

    public ThrownMinionState(Minion m) {
        minion = m;
    }

    public void InitializeState() {
        minion.ApplyGravity(data.gravity,false);
        minion.ToggleCollider(true);
    }

    public void ExitState() {
        minion.ToggleCollider(true);
    }

    public void UpdateState() {
        if(minion.grounded)
            minion.SetState(minion.idleState);
    }

    public void FixedUpdateState() {
        minion.ApplyGravity(data.gravity,false);
    }
}
