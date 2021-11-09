using System;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    public enum Scene { 
        Title, Loading, Gameover,
        CombatArea, RestingArea
    }

    private static Action onLoadCallback;

    public static void Load(Scene scene) {
        onLoadCallback = () => {
            SceneManager.LoadSceneAsync(scene.ToString());
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void Load() {
        var scene = SceneManager.GetActiveScene().name;
        onLoadCallback = () => {
            SceneManager.LoadSceneAsync(scene);
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback() {
        if (onLoadCallback != null) {
            onLoadCallback();
            onLoadCallback = null;
        }
    }

    public static string GetSceneName() { 
        return SceneManager.GetActiveScene().name;
    }
}