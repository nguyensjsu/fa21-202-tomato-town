using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameComponent
{
    private List<IGameComponent> components = new List<IGameComponent>();
    private static GameManager _instance;
    public static GameManager gameInstance { get { return _instance; } }

    private void Awake() {
        if(_instance != null && _instance != this) 
            Destroy(gameObject);
        else { _instance = this; }
    }

    private void Update() {
        UpdateComponent();
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

}
