using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    private void Update() {
        if(PlayerInput.confirm.isPressed) {
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
