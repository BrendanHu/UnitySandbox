using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public Vector2 spawnLocation;
    public GameObject pipe;
    public float respawnTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        //spawnLocation = transform.position;
        
        StartCoroutine(spawnPipes());    
    }
    
    // Die when player makes contact, stopping further spawning
    private void OnCollisionEnter2D(Collision2D collision) {
        var player = collision.collider.GetComponent<Character>();
        if (player != null) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private IEnumerator spawnPipes() {
        while(true) {
            //GameObject spawned;
            spawnLocation = new Vector2(3, Random.Range(-2, 1));
            Instantiate(pipe, spawnLocation, Quaternion.identity);
            print("spawning pipe");
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
