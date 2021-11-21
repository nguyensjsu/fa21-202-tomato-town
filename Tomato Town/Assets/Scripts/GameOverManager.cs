using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour {

    // REMOVE ME LATER AND READ FROM SAVE FILE
    [SerializeField] SceneLoader.Scene room;

    private void Update() {
        if(PlayerInput.confirm.isPressed) {
            ContinueGame();
        }
    }

    public void ContinueGame() {
        SceneLoader.Load(room);
    }

    public void ReturnToTitle() {
        SceneLoader.Load(SceneLoader.Scene.Title);
    }
}
