using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDefaultEnemyState : MonoBehaviour, IAgentState
{
    EnemyAgent user;

    public TestDefaultEnemyState(EnemyAgent e) {
        this.user = e;
    }

    public void InitializeState() { }

    public void UpdateState() { }

    public void FixedUpdateState() {
        user.ApplyGravity(30);
    }

    public void ExitState() { }
}
