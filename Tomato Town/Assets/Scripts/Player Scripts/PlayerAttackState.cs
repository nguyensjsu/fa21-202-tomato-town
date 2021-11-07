using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : AttackBaseState
{
    public PlayerAttackState(Player p, Attack a) : base(p, a) { }

    public override void UpdateState() {
        Attack(GameManager.gameInstance.enemyAgents);
    }

    public override void FixedUpdateState() {
        // Allow for player movement here later
    }
}
