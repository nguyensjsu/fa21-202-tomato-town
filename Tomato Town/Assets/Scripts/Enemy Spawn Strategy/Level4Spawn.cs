using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Spawn : BaseSpawner
{
    Timer timer, timer2;
    bool spawnedLimit => spawnCount > 15;
    float spawnInterval = 1, spawnInterval2 = 4f;

    public override void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        base.InitEnemySpawns(s,f);
        timer = new Timer();
        timer2 = new Timer();
        SpawnRandom();
    }

    public override bool CanAdvance() {
        return spawnedLimit && GameManager.gameInstance.enemyAgents.Count <= 0;
    }

    public override void UpdateSpawns() {
        if(spawnedLimit) return;

        if(timer.WaitForXSeconds(spawnInterval)) {
            SpawnRandom();
        }
        if(timer2.WaitForXSeconds(spawnInterval2)) {
            SpawnSkeleton(true);
        }
    }
}
