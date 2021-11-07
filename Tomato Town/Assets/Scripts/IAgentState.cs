using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentState 
{
    void InitializeState();
    void UpdateState();
    void FixedUpdateState();
    void ExitState();
}
