using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSpawn : BaseSpawner
{
    Timer timer;
    bool spawnedLimit => spawnCount > 20;
    int spawnInterval = 1;

    public override void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        base.InitEnemySpawns(s,f);
        timer = new Timer();
        SpawnRandom();
    }

    public override bool CanAdvance() {
        return spawnedLimit && GameManager.gameInstance.enemyAgents.Count <= 0;
    }

    public override void UpdateSpawns() {
        if(spawnedLimit) return;

        if(timer.WaitForXSeconds(spawnInterval)) {
            SpawnRandom(true);
        }
    }
}
