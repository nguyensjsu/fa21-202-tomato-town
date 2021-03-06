using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PhysicsAgent : MonoBehaviour, IGameComponent
{
    [HideInInspector]
    public Vector2 velocity;
    protected int layerMask;
    protected Rigidbody2D rb;
    protected BoxCollider2D box;
    protected bool isGrounded;
    protected float maxFallSpeed = -1;

    // Collision Variables
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private const float collisionTolerance = 0.01f;
    private Vector2 groundNormal;

    public abstract void UpdateComponent();
    public abstract void FixedUpdateComponent();


    private void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        if(rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected void Awake() {
        isGrounded = false;
        contactFilter.useTriggers = false;
        layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useLayerMask = true;
    }

    // Update collision layer for the entity
    protected void UpdateLayerMask(int layer) {
        layerMask = Physics2D.GetLayerCollisionMask(layer);
        contactFilter.SetLayerMask(layerMask);
    }

    // Move the entity's position and check for collision
    public void Move(Vector2 movement) {
        isGrounded = false;
        MoveEntity(movement * Vector2.right, false);
        MoveEntity(movement * Vector2.up,true);
    }

    private void MoveEntity(Vector2 movement,bool yMovement) {
        float distance = movement.magnitude;

        if(distance > 0) {
            int count = rb.Cast(movement,contactFilter,hitBuffer,distance + collisionTolerance);
            if(movement.y < 0) UpdateDropdown(count);

            for(int i = 0; i < count; i++) {
                if(CheckPassThrough(hitBuffer[i].collider)) continue;

                Vector2 currentNormal = hitBuffer[i].normal;
                if(currentNormal.y > 0.65f) { // Value is the most supported slope
                    isGrounded = true;
                    if(yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity,currentNormal);
                if(projection < 0) velocity -= projection * currentNormal;

                float modifiedDistance = hitBuffer[i].distance - collisionTolerance;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb.position += movement.normalized * distance;
    }

    // Apply gravity to the entity
    public void ApplyGravity(float gravity, bool decelX = true) {
        velocity.y -= gravity * Time.deltaTime;
        if(maxFallSpeed > 0 && velocity.y < 0) velocity.y = Mathf.Max(velocity.y,-maxFallSpeed);

        if(decelX && velocity.x != 0) {
            var prevVel = velocity;
            float deceleration = 100;
            deceleration *= Mathf.Sign(velocity.x);
            velocity.x -= deceleration * Time.deltaTime;
            if(Mathf.Sign(velocity.x * prevVel.x) <= 0) velocity.x = 0;
        }
        Move(velocity * Time.deltaTime);
    }

    // Relocate the object to a given position
    public void RelocateObject(Vector2 newPosition) {
        transform.localPosition = newPosition;
        rb.position = newPosition;
    }

    // Place object onto ground
    public void StickToGround() {
        int count = rb.Cast(Vector2.down,contactFilter,hitBuffer);
        if(count <= 0) return;
        isGrounded = true;
        int index = 0;
        while(index < count) {
            if(!hitBuffer[index].collider.isTrigger) {
                break;
            }
            index += 1;
        }

        float distance = hitBuffer[index].distance - collisionTolerance;
        rb.position += Vector2.down * distance;
    }


    public void AddChild(IGameComponent c) { }

    public void RemoveChild(IGameComponent c) { }

    #region Platform Pass Through Logic
    // Idea: Any colliders in this list are not detected for collision
    protected HashSet<Collider2D> passThroughColliders = new HashSet<Collider2D>();

    private bool CheckPassThrough(Collider2D c) {
        return passThroughColliders.Contains(c);
    }

    public void AddPassThrough(Collider2D c) {
        if(!passThroughColliders.Contains(c))
            passThroughColliders.Add(c);
    }

    public void RemovePassThrough(Collider2D c) {
        if(passThroughColliders.Contains(c))
            passThroughColliders.Remove(c);
    }


    private bool canDrop = false;
    public void EnableDropdown() { canDrop = true; }

    protected void UpdateDropdown(int count) {
        if(!canDrop) return;
        canDrop = false;

        // check if there's plaforms below agent
        for(int i = 0; i < count; i++) {
            if(hitBuffer[i].collider.gameObject.CompareTag("platform"))
                AddPassThrough(hitBuffer[i].collider);
        }
    }
    #endregion

}