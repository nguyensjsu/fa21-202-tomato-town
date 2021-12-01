using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    private static PlayerHUD _instance;
    public static PlayerHUD instance { get { return _instance; } }

    [SerializeField] Image[] hpIcons;
    [SerializeField] Sprite hpFull, hpEmpty;
    [SerializeField] TextMeshProUGUI coins, minions;

    private void Awake() {
        if(_instance != null && _instance != this)
            Destroy(gameObject);
        else { _instance = this; }
        UpdateMinions();
    }

    public void UpdateHP(int curHP) {
        for(int i = 0; i < hpIcons.Length; i++) {
            hpIcons[i].sprite = i >= curHP ? hpEmpty : hpFull;
        }
    }

    public void UpdateCoins(int value) {
        coins.text = value.ToString();
    }

    public void UpdateMinions() {
        minions.text = GameData.minionCount.ToString();
    }
}
