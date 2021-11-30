using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameComponent
{
    private List<IGameComponent> components = new List<IGameComponent>();
    private static GameManager _instance;

    public static GameManager gameInstance { get { return _instance; } }
    public Player playerAgent;
    public List<BaseAgent> enemyAgents = new List<BaseAgent>();
    public EnemyAgent skeletonPrefab, flyguyPrefab;

    IEnemySpawnStrategy spawner;
    private bool gameEnded;

    private void Awake() {
        if(_instance != null && _instance != this) 
            Destroy(gameObject);
        else { _instance = this; }

        if(!playerAgent) 
            playerAgent = FindObjectOfType<Player>();
        AddChild(playerAgent);

        spawner = GetComponent<IEnemySpawnStrategy>();
        if(spawner == null) Debug.Log("Forgot to setup spawner!"); 
        spawner.InitEnemySpawns(skeletonPrefab, flyguyPrefab);
    }

    // Call when player dies
    public void EndGame() {
        if(gameEnded) return;

        gameEnded = true;
        ScreenFader.instance.SlowFadeOut();
        StartCoroutine(WaitToExit(3f,SceneLoader.Scene.Gameover));
    }

    IEnumerator WaitToExit(float time, SceneLoader.Scene exitScene) {
        yield return new WaitForSeconds(time);
        SceneLoader.Load(exitScene);
    }

    public void ExitScene(SceneLoader.Scene nextScene) {
        GameData.playerCoins = playerAgent.curHP;
        GameData.playerCoins = playerAgent.coins;
        ScreenFader.instance.FadeOut();
        //SceneLoader.Load(nextScene);
        StartCoroutine(WaitToExit(0.5f, nextScene));
    }

    private void Update() {
        UpdateComponent();
        spawner.UpdateSpawns();

        if(gameEnded) return;
        if(spawner.CanAdvance()) {
            gameEnded = true;
            GameData.targetScene = SceneLoader.Scene.CombatArea;
            ExitScene(SceneLoader.Scene.RestingArea);
        }
    }

    private void FixedUpdate() {
        FixedUpdateComponent();
    }

    public void UpdateComponent() {
        for(int i = 0; i < components.Count; i++)
            components[i].UpdateComponent();
    }

    public void FixedUpdateComponent() {
        for(int i = 0; i < components.Count; i++)
            components[i].FixedUpdateComponent();
    }

    public void AddChild(IGameComponent c) {
        if(!components.Contains(c)) components.Add(c);
    }

    public void RemoveChild(IGameComponent c) {
        if(components.Contains(c)) components.Remove(c);
    }

    public void AddEnemy(EnemyAgent b) {
        AddChild(b);
        if(!enemyAgents.Contains(b)) enemyAgents.Add(b);
    }

    public void RemoveEnemy(EnemyAgent b) {
        RemoveChild(b);
        if(enemyAgents.Contains(b)) enemyAgents.Remove(b);
    }
}
