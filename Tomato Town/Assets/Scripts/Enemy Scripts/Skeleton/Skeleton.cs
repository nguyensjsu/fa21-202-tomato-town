using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyAgent
{
    public AttackBaseState attackState;
    public SkeletonData data;

    protected new void Start() {
        base.Start();
        defaultState = new IdleSkeleState(this);
        attackState = new EnemyAttackState(this, data.basic);
        SetState(defaultState);
    }

    public override void Attacked(Vector2 knockback) {
        base.Attacked(knockback);

        /*
        if(state == defaultState) {
            attackState.SetAttack(data.basic);
            SetState(attackState);
        }
        */
    }
}
