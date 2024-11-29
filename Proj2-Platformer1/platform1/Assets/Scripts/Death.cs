using System;
using UnityEngine;
public class Death : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.collider.GetComponent<Character>();
        if (player != null) {
            player.Die();
        }
    }
}