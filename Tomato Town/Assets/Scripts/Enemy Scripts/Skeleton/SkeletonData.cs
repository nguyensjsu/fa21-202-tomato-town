using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonData",menuName = "ScriptableObjects/SkeletonData",order = 1)]
public class SkeletonData : ScriptableObject
{
    public float walkSpeed;
    public float gravity;

    public Attack basic;
}