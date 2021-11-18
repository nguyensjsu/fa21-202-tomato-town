using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseAgent {

    public MinionData data;
    [HideInInspector]
    public Player player;
    public IAgentState itemState;
    public IAgentState thrownState;
    public IAgentState knockoutState;

    public Vector2 prevVelocity;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        GameManager.gameInstance.AddChild(this);
        player = GameManager.gameInstance.playerAgent;

        defaultState = new IdleMinionState(this);
        itemState = new ItemMinionState(this);
        thrownState = new ThrownMinionState(this);
        //SetState(itemState);
        SetState(defaultState);
    }

    public override void FixedUpdateComponent() {
        prevVelocity = velocity;
        base.FixedUpdateComponent();
    }

    public bool CanPickupMinion() {
        return state == defaultState || state == thrownState;
    }

    public void PickupMinion() {
        SetState(itemState);
    }

    public void ThrowMinion(float direction) {
        if(state != itemState) return;
        
        velocity = data.throwVelocity;
        if(player.m_upDirection)
            velocity = data.upThrowVelocity;
        else if(player.m_downDirection)
            velocity = data.downThrowVelocity;
        else if(player.m_isMoving)
            velocity = data.blastVelocity;

        velocity.x *= Mathf.Sign(direction);
        SetState(thrownState);
    }

    private bool canHitEnemies;
    public void ToggleCollider(bool enable) {
        canHitEnemies = enable;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!canHitEnemies) return;

        if(collision.CompareTag("Enemy")) {
            BaseAgent enemy = collision.GetComponent<BaseAgent>();
            var direction = Mathf.Sign(velocity.x);
            var kb = data.knockback; kb.x *= direction;
            enemy.Attacked(kb);
            velocity = data.itemBounce;
            velocity.x *= -direction;
        } else if(collision.CompareTag("wall")) {
            velocity.x = -prevVelocity.x;
            print(velocity);
        }

        /*
        else if(collision.CompareTag("wall")) {
            velocity = data.itemBounce;
            velocity.x *= -Mathf.Sign(velocity.x);
        }
        */
    }
}
