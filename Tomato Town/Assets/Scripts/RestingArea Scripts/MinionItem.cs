using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionItem : PurchasableItem
{
    protected override void DoOnPurchase() {
        GameData.minionCount += 1;
    }
}
