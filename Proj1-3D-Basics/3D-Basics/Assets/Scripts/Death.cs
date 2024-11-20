using System;
using UnityEngine;

public class Death : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        var player = other.collider.GetComponent<Character>();
        if (player != null) {
            player.Die();
        }
    }
}
