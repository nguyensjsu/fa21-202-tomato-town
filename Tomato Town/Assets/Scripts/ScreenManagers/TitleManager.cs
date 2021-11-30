using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class TitleManager : MonoBehaviour
{
    private void Update() {
        if(PlayerInput.jump.isPressed) {
            SoundManager.instance.PlayKeyPress();
            NewGame();
        }
    }

    public void NewGame() {
        GameData.InitializeData();
        SceneLoader.Load(SceneLoader.Scene.CombatArea);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
