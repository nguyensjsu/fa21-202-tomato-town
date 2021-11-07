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
        print("poop");
    }

    public void FixedUpdateState() {
        if(player.grounded) {
            Movement(data.walkSpeed,true);
        } else {
            Movement(data.airSpeed,false);
            //HandleJumpFloat();
        }
        player.ApplyGravity(data.gravity);
    }

    public void ExitState() { }

    #region Helper Functions

    protected void Movement(float speed, bool flip) {
        if(!player.m_isMoving) return;
        var dir = (int)Mathf.Sign(player.m_moveInput.x);
        if(flip) player.FaceDirection(dir);
        player.Move(Vector2.right * (dir * speed * Time.deltaTime));
    }

    #endregion
}
