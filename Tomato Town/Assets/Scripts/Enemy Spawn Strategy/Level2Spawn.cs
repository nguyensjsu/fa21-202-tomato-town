using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Spawn : BaseSpawner
{
    Timer timer;
    bool spawnedLimit => spawnCount > 7;
    int spawnInterval = 1;

    public override void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        base.InitEnemySpawns(s,f);
        timer = new Timer();
        SpawnSkeleton();
    }

    public override bool CanAdvance() {
        return spawnedLimit && GameManager.gameInstance.enemyAgents.Count <= 0;
    }

    public override void UpdateSpawns() {
        if(spawnedLimit) return;

        if(timer.WaitForXSeconds(spawnInterval)) {
            if(spawnCount%2 == 0) SpawnSkeleton();
            else SpawnFlyGuy();
        }
    }
}
