using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Spawn : BaseSpawner
{
    Timer timer, timer2;
    bool spawnedLimit => spawnCount > 10;
    float spawnInterval = 1, spawnInterval2 = 2.9f;

    public override void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        base.InitEnemySpawns(s,f);
        timer = new Timer();
        timer2 = new Timer();
        SpawnSkeleton();
    }

    public override bool CanAdvance() {
        return spawnedLimit && GameManager.gameInstance.enemyAgents.Count <= 0;
    }

    public override void UpdateSpawns() {
        if(spawnedLimit) return;

        if(timer.WaitForXSeconds(spawnInterval)) {
            if(spawnCount % 2 == 0) SpawnSkeleton();
            else SpawnFlyGuy();
        }

        if(timer2.WaitForXSeconds(spawnInterval2)) {
            SpawnSkeleton();
        }
    }
}
