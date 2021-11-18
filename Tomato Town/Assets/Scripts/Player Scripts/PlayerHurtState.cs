using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    Timer timer = new Timer();
    bool canAct => timer.ElapsedTime() > data.hurtStun;

    public PlayerHurtState(Player p) {
        this.player = p;
    }

    public void InitializeState() {
        timer.ResetTimer();
        player._animator.SetBool("hurt", true);
    }

    public void ExitState() {
        player._animator.SetBool("hurt", false);
    }

    public void UpdateState() {
        player._animator.SetBool("hurt", !canAct);
        if(timer.WaitForXSeconds(data.hurtStun)) {
            player.SetState(player.defaultState);
        } else if(canAct) {
            player.defaultState.UpdateState();
        } else player.ApplyGravity(data.gravity);
    }

    public void FixedUpdateState() {
        if(canAct) {
            player.defaultState.FixedUpdateState();
        }
    }

}
