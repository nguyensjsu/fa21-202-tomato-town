using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLooper : MonoBehaviour
{
    private static BGMLooper _instance;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if(_instance != null && _instance != this)
            Destroy(gameObject);
        else { _instance = this; }
    }
}
