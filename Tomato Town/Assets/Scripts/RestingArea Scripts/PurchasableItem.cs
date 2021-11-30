using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PurchasableItem : MonoBehaviour, IGameComponent
{
    [SerializeField] int price;
    [SerializeField] TextMeshProUGUI priceDisplay;
    private bool playerDetected;
    private bool purchaseInput => 
        PlayerInput.moveInput.y < -0.4f
        && PlayerInput.jump.isPressed;

    void Start() {
        GameManager.gameInstance.AddChild(this);
        priceDisplay.text = "$" + price.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            playerDetected = false;
        }
    }

    protected abstract void DoOnPurchase();

    public void UpdateComponent() {
        if(purchaseInput && playerDetected) {
            Player player = GameManager.gameInstance.playerAgent;
            if(player.coins >= price) {
                player.AdjustCoinAmount(-price);
                SoundManager.instance.PlayPickup();
                DoOnPurchase();
            }
        }
    }

    public void FixedUpdateComponent() { }
    public void AddChild(IGameComponent c) { }
    public void RemoveChild(IGameComponent c) { }
}
