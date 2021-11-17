using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveMinionState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    public HaveMinionState(Player p) {
        this.player = p;
    }

    public void InitializeState() { }
    public void ExitState() { }


    public void UpdateState() {
        //player.defaultState.UpdateState();

        if(player.m_item) {
            player.minion.ThrowMinion(player.transform.localScale.x);
            player.minion = null;
            player.SetSubState(player.noMinionState);
        }
    }

    public void FixedUpdateState() {
        //player.defaultState.FixedUpdateState();
    }

}
