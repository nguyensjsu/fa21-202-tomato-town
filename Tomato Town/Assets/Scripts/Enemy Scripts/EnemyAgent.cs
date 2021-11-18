using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAgent : BaseAgent
{
    protected LayerMask baseLayerMask;
    private float castLength;
    protected Vector2 shift;

    protected new void Start() {
        base.Start();
        baseLayerMask = layerMask;
        UpdateLayerMask(LayerMask.NameToLayer("EnemyMovement"));
        tag = "Enemy";

        var layer = LayerMask.NameToLayer("Enemy");
        layerMask = Physics2D.GetLayerCollisionMask(layer);
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;

        castLength = 0.25f;
        var offset = box.offset;
        var size = box.size;
        shift.x = (offset + size * 0.5f).x;
        shift.y = (offset - size * 0.5f).y;
        shift *= transform.localScale;

        GameManager.gameInstance.AddEnemy(this);
    }

    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[32];
    public bool SearchPlayer(float searchRadius) {
        int count = Physics2D.CircleCast(transform.position,searchRadius,Vector2.zero,contactFilter,hitBuffer);
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

    // Move the entity horizontally
    protected bool MoveForward(float moveSpeed) { return MoveHorizontally(moveSpeed,transform.localScale.x); }
    protected bool MoveBackward(float moveSpeed) { return MoveHorizontally(moveSpeed,-transform.localScale.x); }
    protected bool MoveHorizontally(float moveSpeed,float direction,float castLen = -1) {
        var dir = Mathf.Sign(direction);
        castLen = castLen < 0 ? castLength : castLen;
        if(!CheckPatrolDirection(castLen,dir)) return false;

        Move(Vector2.right * (dir * moveSpeed * Time.deltaTime));
        return true;
    }

    // Returns true if the enemy can continue moving towards the given direction
    protected bool CheckPatrolDirection(float castDistance,float currentDirection) {
        Vector2 direction = Vector2.down;
        direction.x = Mathf.Sign(currentDirection);

        Vector2 position = transform.position;
        shift.x = Mathf.Abs(shift.x) * Mathf.Sign(currentDirection);
        position += shift;

        var checkGround = Physics2D.Raycast(position,direction,castDistance,layerMask);
        var checkWall = Physics2D.Raycast(position,direction * Vector2.right,castDistance,layerMask);

        Debug.DrawLine(position,position + (castDistance * direction));
        //print(checkWall.collider == false);

        return checkGround.collider == true && checkWall.collider == false;
    }
    #endregion

}
