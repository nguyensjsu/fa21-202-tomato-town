using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData",menuName = "ScriptableObjects/PlayerData",order = 1)]
public class PlayerData : ScriptableObject
{
    public float walkSpeed, airSpeed;
    public float gravity, jumpForce, maxFallSpeed;
    public int jumpHold;

    public Attack basic, downAttack;
    public float flutterForce;

    public float grabRadius;
    public float hurtDuration, hurtStun;
    public Vector2 hurtKnockback;
}