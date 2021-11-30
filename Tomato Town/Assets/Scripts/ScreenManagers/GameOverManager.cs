using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class GameOverManager : MonoBehaviour {

    private void Update() {
        if(PlayerInput.jump.isPressed) {
            SoundManager.instance.PlayKeyPress();
            ContinueGame();
        }
    }

    public void ContinueGame() {
        GameData.InitializeData();
        SceneLoader.Load(GameData.targetScene);
    }

    public void ReturnToTitle() {
        SceneLoader.Load(SceneLoader.Scene.Title);
    }
}
