using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : EnemyAgent
{
    private TestDefaultEnemyState defaultState;

    protected new void Start() {
        base.Start();
        defaultState = new TestDefaultEnemyState(this);
        SetState(defaultState);
    }
}