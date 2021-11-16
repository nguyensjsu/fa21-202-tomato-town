using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    public DefaultPlayerState(Player p) {
        this.player = p;
    }

    public void InitializeState() { }

    public void UpdateState() {
        if(player.grounded && player.m_jumpPress) {
            player.Jump(data.jumpForce,data.airSpeed);
        }
        else if(!player.grounded && player.hasBooster) {
            if(player.m_jumpPress) {
                player.Jump(data.flutterForce,data.airSpeed, false);
                player.hasBooster = false;
            }
        }

        if(player.m_attack) {
            Attack a = data.basic;
            if(!player.grounded && player.m_downDirection)
                a = data.downAttack;

            player.attackState.SetAttack(a);
            player.SetState(player.attackState);
            player.Minion();
        }
    }

    public void FixedUpdateState() {
        if(player.grounded) {
            player.HorizontalMovement(data.walkSpeed, true);
        } else {
            player.HorizontalMovement(data.airSpeed, true);
            player.HandleJumpFloat();
        }
        player.ApplyGravity(data.gravity);
    }

    public void ExitState() { }

    
}
