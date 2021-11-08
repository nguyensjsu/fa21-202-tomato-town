using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : AttackBaseState
{
    Player player;
    PlayerData data => player.data;

    public PlayerAttackState(Player p, Attack a) : base(p, a) {
        this.player = p;
    }

    protected override void PerformOnHit() {
        player.hasBooster = true;
    }

    public override void UpdateState() {
        Attack(GameManager.gameInstance.enemyAgents);

        if(player.grounded && player.m_jumpPress) {
            player.Jump(data.jumpForce, data.airSpeed);
        }
        else if(!player.grounded && player.hasBooster) {
            if(player.m_jumpPress) {
                player.Jump(data.jumpForce,data.airSpeed);
                player.hasBooster = false;
            }
        }
    }

    public override void FixedUpdateState() {
        // Allow for player movement here later
        player.defaultState.FixedUpdateState();
    }
}
