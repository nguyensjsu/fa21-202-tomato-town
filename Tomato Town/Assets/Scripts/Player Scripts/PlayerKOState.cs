using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKOState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    public PlayerKOState(Player p) {
        this.player = p;
    }

    public void InitializeState() {
        player._animator.SetBool("ko", true);
        GameManager.gameInstance.EndGame();

        if(player.hasMinion) {
            player.minion.DropMinion(player.transform.localScale.x);
        }
    }

    public void ExitState() { }

    public void UpdateState() { }

    public void FixedUpdateState() {
        player.ApplyGravity(data.gravity);
    }

}
