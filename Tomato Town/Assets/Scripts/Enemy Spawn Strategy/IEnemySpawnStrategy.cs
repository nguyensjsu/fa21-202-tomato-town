using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnStrategy
{
    void InitEnemySpawns(EnemyAgent type1, EnemyAgent type2);
    void UpdateSpawns();
    bool CanAdvance();
}
