using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{

    // A small function used to delay actions for a certain amount of time
    private float _curTime;

    public Timer() {
        _curTime = 0;
    }

    public void AdvanceTime() {
        _curTime += Time.deltaTime;
        return;

        // Prevent frames from being skipped
        const float maxTime = 1 / 60f;
        if(Time.deltaTime > maxTime) _curTime += maxTime;
        else _curTime += Time.deltaTime;
    }

    public bool WaitForXFrames(int x) {
        AdvanceTime();

        if(!(_curTime * 60 >= x)) return false;
        _curTime = 0; return true;
    }

    public bool WaitForXSeconds(float x) {
        AdvanceTime();

        if(!(_curTime >= x)) return false;
        _curTime = 0; return true;
    }

    public void ResetTimer() { _curTime = 0; }
    public int CurrentFrame() { return (int)(_curTime * 60); }
    public float ElapsedTime() { return _curTime; }
}