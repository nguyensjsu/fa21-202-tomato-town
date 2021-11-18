using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows other scripts to easily adjust the shaders of a character rig
public class ShaderManager : MonoBehaviour
{
    private SpriteRenderer[] renders;
    private Material[] originalMats;

    private MaterialConfig materials => GameManager.gameInstance.materials;

    private Timer timer;

    void Awake() {
        renders = GetComponentsInChildren<SpriteRenderer>();
        originalMats = new Material[renders.Length];
        for(int i = 0; i < renders.Length; i++)
            originalMats[i] = renders[i].material;
    }

    public void FlashWhite() {
        //StartCoroutine(FlashWhite(dungeon.constants.hurtTime));
    }

    public void ApplyInvincibleMaterial() { ChangeMaterial(materials.blinkShader); }

    private IEnumerator FlashWhite(float waitTime) {
        ChangeMaterial(materials.hitlagShader);
        yield return new WaitForSeconds(waitTime);
        RevertMaterial();
    }

    private void ChangeMaterial(Material m) {
        if(m == null) return;
        for(int i = 0; i < renders.Length; i++)
            renders[i].material = m;
    }

    public void RevertMaterial() {
        for(int i = 0; i < renders.Length; i++)
            renders[i].material = originalMats[i];
    }
}