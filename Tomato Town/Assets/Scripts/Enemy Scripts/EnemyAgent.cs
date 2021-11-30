using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAgent : BaseAgent
{
    protected LayerMask baseLayerMask;
    private float castLength;
    protected Vector2 shift;

    private Attack bodyHitbox;
    public IAgentState koState;

    protected new void Start() {
        base.Start();
        UpdateLayerMask(LayerMask.NameToLayer("EnemyMovement"));
        baseLayerMask = layerMask;
        tag = "Enemy";

        var layer = LayerMask.NameToLayer("Enemy");
        layerMask = Physics2D.GetLayerCollisionMask(layer);
        hitFilter.SetLayerMask(layerMask);
        hitFilter.useLayerMask = true;
        hitFilter.useTriggers = true;

        bodyHitbox = new Attack();
        bodyHitbox.knockback.x = 1;

        castLength = 0.25f;
        var offset = box.offset;
        var size = box.size;
        shift.x = (offset + size * 0.5f).x;
        shift.y = (offset - size * 0.5f).y;
        shift *= transform.localScale;

        GameManager.gameInstance.AddEnemy(this);
    }

    public override void Attacked(Vector2 knockback,int damage = 1) {
        if(state == koState) return;

        base.Attacked(knockback,damage);
        if(curHP <= 0) SetState(koState);
        SoundManager.instance.PlayHurtE();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(curHP <= 0) return;

        if(collision.CompareTag("Player")) {
            BaseAgent player = collision.GetComponent<BaseAgent>();
            var direction = Mathf.Sign(velocity.x);
            var kb = bodyHitbox.knockback; kb.x *= direction;
            player.Attacked(kb);
        }
    }

    private ContactFilter2D hitFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[32];
    public bool SearchPlayer(float searchRadius) {
        int count = Physics2D.CircleCast(transform.position,searchRadius,Vector2.zero,hitFilter,hitBuffer);
        for(int i = 0; i < count; i++) {
            if(hitBuffer[i].collider.gameObject.CompareTag("Player")) {
                return true;
            }
        }
        return false;
    }

    #region Movement Functions

    public void FacePlayer() {
        var playerPos = GameManager.gameInstance.playerAgent.transform.position;
        var enemyPos = transform.position;
        FaceDirection(playerPos.x - enemyPos.x);
    }

    public void FlipEnemy() {
        FaceDirection(-transform.localScale.x);
    }

    // Move the entity horizontally
    public bool MoveForward(float moveSpeed) { return MoveHorizontally(moveSpeed,transform.localScale.x); }
    public bool MoveBackward(float moveSpeed) { return MoveHorizontally(moveSpeed,-transform.localScale.x); }
    public bool MoveHorizontally(float moveSpeed,float direction,float castLen = -1) {
        var dir = Mathf.Sign(direction);
        castLen = castLen < 0 ? castLength : castLen;
        if(!CheckPatrolDirection(castLen,dir)) return false;

        Move(Vector2.right * (dir * moveSpeed * Time.deltaTime));
        return true;
    }

    // Returns true if the enemy can continue moving towards the given direction
    public bool CheckPatrolDirection(float castDistance,float currentDirection) {
        Vector2 direction = Vector2.down;
        direction.x = Mathf.Sign(currentDirection);

        Vector2 position = transform.position;
        shift.x = Mathf.Abs(shift.x) * Mathf.Sign(currentDirection);
        position += shift;

        var checkWall = Physics2D.Raycast(position,direction * Vector2.right,castDistance,baseLayerMask);

        Debug.DrawLine(position,position + (castDistance * direction));
        //print(checkWall.collider == false);

        return checkWall.collider == false;
    }
    #endregion

}
