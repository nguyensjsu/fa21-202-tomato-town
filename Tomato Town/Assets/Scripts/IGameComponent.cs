using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameComponent
{
    void AddChild(IGameComponent c);
    void RemoveChild(IGameComponent c);
    void UpdateComponent();
    void FixedUpdateComponent();
}
