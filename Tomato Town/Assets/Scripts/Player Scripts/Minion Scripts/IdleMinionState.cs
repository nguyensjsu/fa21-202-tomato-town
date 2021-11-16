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
    }
    public void ExitState() { }
    public void UpdateState() { }

    public void FixedUpdateState() { }

}
