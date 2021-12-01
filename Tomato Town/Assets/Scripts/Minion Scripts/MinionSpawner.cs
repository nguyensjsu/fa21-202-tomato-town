using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] Minion minionPrefab;
    [SerializeField] float minX, maxX, spawnHeight;

    private void Awake() {
        for(int i = 0; i < GameData.minionCount; i++) {
            SpawnMinion();
        }
    }

    void SpawnMinion() {
        var xVal = Random.Range(minX, maxX);
        var pos = new Vector2(xVal,spawnHeight);
        var m = Instantiate(minionPrefab,pos,Quaternion.identity);
    }
}
