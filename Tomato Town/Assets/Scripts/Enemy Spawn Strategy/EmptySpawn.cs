using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpawn : MonoBehaviour, IEnemySpawnStrategy
{

    public void InitEnemySpawns(EnemyAgent s,EnemyAgent f) { }
    public bool CanAdvance() { return true; }
    public void UpdateSpawns() { }

}
