using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAgent : PhysicsAgent
{
    protected IAgentState state { get; private set; }

    public void SetState(IAgentState state) {
        this.state = state;
    }
}
