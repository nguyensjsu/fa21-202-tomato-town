using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    Timer timer = new Timer();
    bool canAct => timer.ElapsedTime() > data.hurtStun;

    SpriteRenderer[] renders;


    public PlayerHurtState(Player p) {
        this.player = p;
        renders = player.GetComponentsInChildren<SpriteRenderer>();
    }

    public void InitializeState() {
        timer.ResetTimer();
        player._animator.SetBool("hurt", true);
    }

    public void ExitState() {
        player._animator.SetBool("hurt", false);
        SetSpriteAlpha(1);
    }

    public void UpdateState() {
        player._animator.SetBool("hurt", !canAct);
        if(Time.frameCount % data.blinkSpeed == 0) SetSpriteAlpha(0);
        else SetSpriteAlpha(1);

        if(timer.WaitForXSeconds(data.hurtDuration)) {
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

    private void SetSpriteAlpha(float a) {
        for(int i = 0; i < renders.Length; i++) {
            var color = renders[i].color;
            color.a = a;
            renders[i].color = color;
        }
    }
}
