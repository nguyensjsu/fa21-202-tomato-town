using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObserver
{
    void UpdateObserver();

    void FixedUpdateObserver();

}
