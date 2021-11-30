using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            //SceneLoader.Load(SceneLoader.Scene.RestingArea);
            GameManager.gameInstance.ExitScene(SceneLoader.Scene.RestingArea);
        }
    }
}
