using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerState : MonoBehaviour, IAgentState
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
    }

    public void FixedUpdateState() {
        if(player.grounded) {
            player.HorizontalMovement(data.walkSpeed, true);
        } else {
            player.HorizontalMovement(data.airSpeed, false);
            player.HandleJumpFloat();
        }
        player.ApplyGravity(data.gravity);
    }

    public void ExitState() { }

    
}
