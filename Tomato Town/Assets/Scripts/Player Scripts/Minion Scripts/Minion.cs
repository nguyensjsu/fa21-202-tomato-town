using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseAgent {

    public MinionData data;
    [HideInInspector]
    public BaseAgent player;
    public IAgentState idleState;
    public IAgentState itemState;
    public IAgentState knockoutState;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        GameManager.gameInstance.AddChild(this);
        player = GameManager.gameInstance.playerAgent;

        idleState = new IdleMinionState(this);
        itemState = new ItemMinionState(this);
        SetState(itemState);
    }
}
