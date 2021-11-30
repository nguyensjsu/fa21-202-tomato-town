using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyGuyData",menuName = "ScriptableObjects/FlyGuyData",order = 1)]
public class FlyGuyData : ScriptableObject
{
    public int hp;
    public float walkSpeed;
    public float gravity;

    public int minReward, maxReward;
}