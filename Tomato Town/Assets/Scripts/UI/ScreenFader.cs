using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private static ScreenFader _instance;
    public static ScreenFader instance { get { return _instance; } }
    private Animator animator;


    private void Awake() {
        if(_instance != null && _instance != this)
            Destroy(gameObject);
        else { _instance = this; }
        animator = GetComponent<Animator>();
    }

    public void FadeOut() {
        animator.SetTrigger("fadeOut");
    }

    public void SlowFadeOut() {
        animator.SetTrigger("slowFadeOut");
    }
}
