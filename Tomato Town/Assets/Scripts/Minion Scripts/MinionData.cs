using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinionData",menuName = "ScriptableObjects/MinionData",order = 1)]
public class MinionData : ScriptableObject
{
    public float walkSpeed, airSpeed;
    public float gravity, jumpForce, maxFallSpeed;

    public Vector2 throwVelocity, upThrowVelocity, downThrowVelocity, blastVelocity;
    public Vector2 itemShift, itemBounce, itemGroundBounce, knockback;
}