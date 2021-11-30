using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : PurchasableItem {
    protected override void DoOnPurchase() {
        GameManager.gameInstance.playerAgent.AdjustHealth(1);
    }
}
