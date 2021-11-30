using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int playerHP;
    public static int playerCoins;
    public static int minionCount;
    public static SceneLoader.Scene targetScene;

    public static void InitializeData() {
        playerHP = 3;
        playerCoins = 0;
        minionCount = 1;
        targetScene = SceneLoader.Scene.CombatArea;
    }
}
