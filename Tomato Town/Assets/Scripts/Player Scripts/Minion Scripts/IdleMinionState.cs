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

    public void InitializeState() { }
    public void ExitState() { }
    public void UpdateState() { }

    public void FixedUpdateState() {
        minion.ApplyGravity(data.gravity, false);
    }

}
