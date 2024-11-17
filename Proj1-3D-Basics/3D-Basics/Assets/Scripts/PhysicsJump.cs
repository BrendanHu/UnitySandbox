using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float jumpForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        } 
    }
}
