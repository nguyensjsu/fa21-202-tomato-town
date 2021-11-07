using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSubject 
{
    /**
     * Add a Game Observer
     * @param obj Observer Object
     */
    void RegisterObserver(IGameObserver obj);

    /**
     * Remove Observer
     * @param obj Game Observer to Remove
     */
    void RemoveObserver(IGameObserver obj);

    /**
     * Broadcast Event to Observers
     */
    void NotifyObserver();
}
