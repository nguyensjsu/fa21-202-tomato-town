using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] bool canDropdown;
    [SerializeField] Collider2D[] colliders;
    public bool AllowDropdown => canDropdown; 

    private bool IsAgentLayer(string tag) {
        if(tag == "Player") return true;
        if(tag == "Enemy") return true;
        if(tag == "Minion") return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(IsAgentLayer(collision.gameObject.tag)) {
            PhysicsAgent agent = collision.gameObject.GetComponent<PhysicsAgent>();
            for(int i = 0; i < colliders.Length; i++) {
                agent.AddPassThrough(colliders[i]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(IsAgentLayer(collision.gameObject.tag)) {
            PhysicsAgent agent = collision.gameObject.GetComponent<PhysicsAgent>();
            for(int i = 0; i < colliders.Length; i++) {
                agent.RemovePassThrough(colliders[i]);
            }
        }
    }
}
