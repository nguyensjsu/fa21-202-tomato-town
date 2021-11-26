using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyGuy : EnemyAgent
{
    public IAgentState koState;
    public FlyGuyData data;

    protected new void Start() {
        base.Start();
        defaultState = new IdleFlyState(this);
        koState = new KOFlyState(this);
        //attackState = new EnemyAttackState(this,data.basic);
        SetState(defaultState);
        InitializeHealth(data.hp);
    }

    public override void Attacked(Vector2 knockback, int damage = 1) {
        if(state == koState) return;

        base.Attacked(knockback, damage);
        if(curHP <= 0) SetState(koState);
    }
}
