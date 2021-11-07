using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PhysicsAgent : MonoBehaviour, IGameComponent
{
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
        Vector2 moveAlongGround = new Vector2(groundNormal.y,-groundNormal.x);
        moveAlongGround = moveAlongGround.normalized;
        Vector2 move = moveAlongGround * movement.x;
        MoveEntity(move,false);
        MoveEntity(movement * Vector2.up,true);
    }

    private void MoveEntity(Vector2 movement,bool yMovement) {
        float distance = movement.magnitude;

        if(distance > 0) {
            int count = rb.Cast(movement,contactFilter,hitBuffer,distance + collisionTolerance);

            for(int i = 0; i < count; i++) {
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

    public void AddChild(IGameComponent c) { }

    public void RemoveChild(IGameComponent c) { }
}