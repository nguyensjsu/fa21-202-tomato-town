using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnStrategy
{
    void InitEnemySpawns();
    void UpdateSpawns();
    bool CanAdvance();
}
