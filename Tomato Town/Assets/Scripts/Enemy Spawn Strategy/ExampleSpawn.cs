using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSpawn : MonoBehaviour, IEnemySpawnStrategy
{
    EnemyAgent skelePrefab, flyguyPrefab;
    float leftSpawn = -15f, rightSpawn = 15f;
    Timer timer;
    int spawnCount;

    public bool CanAdvance() {
        return spawnCount > 2;
    }

    public void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        skelePrefab = s;
        flyguyPrefab = f;
        timer = new Timer();
        SpawnRandom();
    }

    public void UpdateSpawns() {
        if(timer.WaitForXSeconds(3)) {
            if(GameManager.gameInstance.enemyAgents.Count < 5)
                SpawnRandom();
        }
    }

    private void SpawnRandom() {
        if(spawnCount > 2) return;

        var a = Random.Range(0,4);
        var b = Random.Range(0,2);
        var mob = a != 0 ? skelePrefab : flyguyPrefab;
        SpawnEnemy(mob, b == 0);
        spawnCount += 1;
    }

    private void SpawnEnemy(EnemyAgent mob, bool isLeft) {
        var pos = mob.transform.position;
        pos.x = isLeft ? leftSpawn : rightSpawn;
        var m = Instantiate(mob,pos,Quaternion.identity);
        if(isLeft) {
            var s = m.transform.localScale;
            s.x *= -1;
            m.transform.localScale = s;
        }

        GameManager.gameInstance.AddEnemy(m);
    }
}
