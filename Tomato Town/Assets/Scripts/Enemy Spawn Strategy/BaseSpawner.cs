using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour, IEnemySpawnStrategy
{
    private EnemyAgent skelePrefab, flyguyPrefab;
    private const float leftSpawn = -15f, rightSpawn = 15f;
    private const float minFlyHeight = 11.6f, maxFlyHeight = 12.7f;
    private const float skydropHeight = 21.24f;
    protected int spawnCount;

    public abstract bool CanAdvance();
    public abstract void UpdateSpawns();

    public virtual void InitEnemySpawns(EnemyAgent s,EnemyAgent f) {
        skelePrefab = s;
        flyguyPrefab = f;
    }

    protected void SpawnRandom(bool skydrop = false) {
        var a = Random.Range(0,2);
        var b = Random.Range(0,2);
        var mob = a != 0 ? skelePrefab : flyguyPrefab;
 
        if(a == 0) SpawnFlyGuy();
        else SpawnSkeleton(skydrop);
    }

    private void SpawnFlyGuy() {
        var mob = flyguyPrefab;
        var pos = mob.transform.position;
        bool isLeft = Random.Range(0,2) == 0;
        pos.x = isLeft ? leftSpawn : rightSpawn;
        pos.y = Random.Range(minFlyHeight, maxFlyHeight);

        var m = Instantiate(mob,pos,Quaternion.identity);
        if(isLeft) FlipMob(m);
        
        GameManager.gameInstance.AddEnemy(m);
        spawnCount += 1;
    }

    private void SpawnSkeleton(bool skydrop = false) {
        var mob = skelePrefab;
        var pos = mob.transform.position;
        bool isLeft = Random.Range(0,2) == 0;

        if(skydrop) {
            pos.x = Random.Range(leftSpawn,rightSpawn);
            pos.y = skydropHeight;
        } else { pos.x = isLeft ? leftSpawn : rightSpawn; }

        var m = Instantiate(mob,pos,Quaternion.identity);
        if(isLeft) FlipMob(m);

        GameManager.gameInstance.AddEnemy(m);
        spawnCount += 1;
    }

    private void FlipMob(EnemyAgent m) {
        var s = m.transform.localScale;
        s.x *= -1;
        m.transform.localScale = s;
    }
    
}
