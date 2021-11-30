using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    private void Awake() {
        NewGame();
    }

    public void NewGame() {
        GameData.InitializeData();
        SceneLoader.Load(SceneLoader.Scene.CombatArea);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
