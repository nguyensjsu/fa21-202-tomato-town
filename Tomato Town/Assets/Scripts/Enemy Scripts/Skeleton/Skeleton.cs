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
        koState = new KOSkeleState(this);
        SetState(defaultState);
        InitializeHealth(data.hp);
    }

    
}
