using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AttackBaseState
{
    EnemyAgent enemy;

    public EnemyAttackState(EnemyAgent p, Attack a) : base(p, a) {
        this.enemy = p;
    }

    public override void UpdateState() {
        Attack(GameManager.gameInstance.enemyAgents);
    }

    public override void FixedUpdateState() { }
}