using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSpawn : MonoBehaviour, IEnemySpawnStrategy
{
    EnemyAgent skelePrefab, flyguyPrefab;
    public float leftSpawn = -15f, rightSpawn = 15f;
    Timer timer = new Timer();

    public ExampleSpawn(EnemyAgent s,EnemyAgent f) {
        skelePrefab = s;
        flyguyPrefab = f;
    }

    public bool CanAdvance() {
        throw new System.NotImplementedException();
    }

    public void InitEnemySpawns() {
        //InvokeRepeating("SpawnRandom",2.0f,1f);
        //InvokeRepeating("SpawnRandom",10.0f,2.9f);
        //SpawnRandom();
    }

    public void UpdateSpawns() {
        if(timer.WaitForXSeconds(3)) {
            if(GameManager.gameInstance.enemyAgents.Count < 5)
                SpawnRandom();
        }
    }

    private void SpawnRandom() {
        var a = Random.Range(0,4);
        var b = Random.Range(0,2);
        var mob = a != 0 ? skelePrefab : flyguyPrefab;
        SpawnEnemy(mob, b == 0);
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
