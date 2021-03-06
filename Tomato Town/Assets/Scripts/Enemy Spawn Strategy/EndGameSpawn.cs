using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSpawn : BaseSpawner
{
    Timer timer, timer2;
    bool spawnedLimit => spawnCount > spawnLimit;
    float spawnInterval = 1, spawnInterval2 = 2.9f;
    int spawnLimit = 1;

    public override void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        base.InitEnemySpawns(s,f);
        timer = new Timer();
        timer2 = new Timer();
        spawnLimit = GameData.level * 2;
        SpawnRandom();
    }

    public override bool CanAdvance() {
        if(spawnLimit <= 1) return false;
        return spawnedLimit && GameManager.gameInstance.enemyAgents.Count <= 0;
    }

    public override void UpdateSpawns() {
        if(spawnedLimit) return;

        if(timer.WaitForXSeconds(spawnInterval)) {
            SpawnRandom(true);
        }
        if(timer2.WaitForXSeconds(spawnInterval2)) {
            SpawnSkeleton(true);
        }
    }
}

