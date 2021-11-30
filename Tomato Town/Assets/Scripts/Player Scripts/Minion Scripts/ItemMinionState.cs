using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMinionState : IAgentState
{
    Minion minion;
    MinionData data => minion.data;

    public ItemMinionState(Minion m) {
        minion = m;
    }

    public void InitializeState() {
        SoundManager.instance.PlayPickup();
    }
    public void ExitState() { }
    public void UpdateState() { }

    // Stick to player's hand
    public void FixedUpdateState() {
        Vector2 targetPosition = minion.player.transform.position;
        Vector2 shift = data.itemShift;
        if(minion.player.transform.localScale.x < 0)
            shift.x *= -1;
        minion.RelocateObject(targetPosition + shift);
    }
}
