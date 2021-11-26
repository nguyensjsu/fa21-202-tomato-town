using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMinionState : IAgentState
{
    Player player;
    PlayerData data => player.data;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[32];
    private ContactFilter2D contactFilter;
    private int layerMask;

    public NoMinionState(Player p) {
        this.player = p;

        var layer = LayerMask.NameToLayer("MinionDetection");
        layerMask = Physics2D.GetLayerCollisionMask(layer);
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;
    }

    public void InitializeState() { }
    public void ExitState() { }


    public void UpdateState() {
        //player.defaultState.UpdateState();
        DrawCircle(player.transform.position,data.grabRadius);


        if(player.m_item) {
            if(SearchMinion()) {
                player.SetSubState(player.haveMinionState);
            }
        }
    }

    public void FixedUpdateState() {
        //player.defaultState.FixedUpdateState();
    }

    private bool SearchMinion() {
        int count = Physics2D.CircleCast(player.transform.position, data.grabRadius, Vector2.zero, contactFilter, hitBuffer);
        for(int i = 0; i < count; i++) {
            if(hitBuffer[i].collider.gameObject.CompareTag("Minion")) {
                Minion m = hitBuffer[i].collider.gameObject.GetComponent<Minion>();
                if(m.CanPickupMinion()) {
                    m.PickupMinion();
                    player.minion = m;
                    return true;
                }
            }
        }
        return false;
    }

    private void DrawCircle(Vector2 origin, float radius) {
        Debug.DrawLine(origin, origin + Vector2.down * radius, Color.green);
        Debug.DrawLine(origin,origin + Vector2.up * radius,Color.green);
        Debug.DrawLine(origin,origin + Vector2.left * radius,Color.green);
        Debug.DrawLine(origin,origin + Vector2.right * radius,Color.green);
        Debug.DrawLine(origin,origin + Vector2.one.normalized * radius,Color.green);
        Debug.DrawLine(origin,origin + Vector2.one.normalized * -radius,Color.green);
    }

}
