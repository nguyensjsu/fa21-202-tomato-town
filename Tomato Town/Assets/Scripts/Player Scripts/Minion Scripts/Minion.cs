using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseAgent {

    public MinionData data;
    [HideInInspector]
    public BaseAgent player;
    public IAgentState idleState;
    public IAgentState itemState;
    public IAgentState thrownState;
    public IAgentState knockoutState;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        GameManager.gameInstance.AddChild(this);
        player = GameManager.gameInstance.playerAgent;

        idleState = new IdleMinionState(this);
        itemState = new ItemMinionState(this);
        thrownState = new ThrownMinionState(this);
        SetState(itemState);
    }

    public void ThrowMinion(float direction) {
        if(state != itemState) return;
        direction = Mathf.Sign(direction);
        velocity = data.throwVelocity;
        velocity.x *= direction;
        SetState(thrownState);
    }


    private bool canHitEnemies;
    public void ToggleCollider(bool enable) {
        canHitEnemies = enable;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!canHitEnemies) return;

        if(collision.tag.CompareTo("Enemy") == 0) {
            BaseAgent enemy = collision.GetComponent<BaseAgent>();
            var direction = Mathf.Sign(velocity.x);
            var kb = data.knockback; kb.x *= direction;
            enemy.Attacked(kb);
            velocity = data.itemBounce;
            velocity.x *= -direction;
        }
    }
}
