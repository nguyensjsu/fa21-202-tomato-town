using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyAgent
{
    public AttackBaseState attackState;
    public IAgentState koState;
    public SkeletonData data;

    protected new void Start() {
        base.Start();
        defaultState = new IdleSkeleState(this);
        attackState = new EnemyAttackState(this, data.basic);
        koState = new KOSkeleState(this);
        SetState(defaultState);
        InitializeHealth(data.hp);
    }

    public override void Attacked(Vector2 knockback, int damage = 1) {
        if(state == koState) return;

        base.Attacked(knockback,damage);
        if(curHP <= 0) SetState(koState);

        /*
        if(state == defaultState) {
            attackState.SetAttack(data.basic);
            SetState(attackState);
        }
        */
    }
}
