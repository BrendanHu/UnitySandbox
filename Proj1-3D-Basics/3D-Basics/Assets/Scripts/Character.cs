using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour {
    public Rigidbody rb;
    
    public float horizontal;
    public float vertical;
    public float maxMoveSpeed = 1;
    public float turnSpeed = 30;
    public float runAcceleration;
    public float runDeceleration;
    public float friction;

    float xVel;
    float yVel;
    public Vector3 movement;
    public float veldebug;

    Animator _animator;
    // Called before first frame update
    private void Start() {
        rb = this.GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }
    // Called once per frame
    private void Update() {
        // get values from keeb
        horizontal = Input.GetAxis("Horizontal") * maxMoveSpeed;
        vertical = Input.GetAxis("Vertical") * maxMoveSpeed;

        // move object
        /*transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed);*/
    }
    private void FixedUpdate() {
        //moveChar(movement);
        float targetHSpeed = horizontal;
        float targetVSpeed = vertical;
        /*print(targetHSpeed); print(targetVSpeed);*/

        float hAccelRate = (Mathf.Abs(targetHSpeed) > 0.01f) ? runAcceleration : runDeceleration;
        float vAccelRate = (Mathf.Abs(targetVSpeed) > 0.01f) ? runAcceleration : runDeceleration;


        float hSpeedDiff = targetHSpeed - rb.velocity.x;
        float vSpeedDiff = targetVSpeed - rb.velocity.z;
        /*print("h"); print(hSpeedDiff);
        print("v"); print(vSpeedDiff);*/

        float hMovement = hSpeedDiff * hAccelRate;
        float vMovement = vSpeedDiff * vAccelRate;
        /*print(hMovement);
        print(vMovement);*/

        #region Friction
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            float hAmount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));
            hAmount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector3.right * -hAmount, ForceMode.Impulse);
        }
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
            float vAmount = Mathf.Min(Mathf.Abs(rb.velocity.z), Mathf.Abs(friction));
            vAmount *= Mathf.Sign(rb.velocity.z);
            rb.AddForce(Vector3.forward * -vAmount, ForceMode.Impulse);
        }
        #endregion
        

        rb.AddForce(hMovement * Vector3.right, ForceMode.Force);
        rb.AddForce(vMovement * Vector3.forward, ForceMode.Force);

        // For animator on movement update
        _animator.SetFloat("speed", rb.velocity.magnitude);
        veldebug = rb.velocity.magnitude;

        /*// Cap the move speed
        if (rb.velocity.magnitude > maxMoveSpeed) {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }*/





    }

    private void moveChar(Vector3 direction) {
        //rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }
}

/*[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {
    private CharacterController characterController;
    
    public float playerSpeed = 4f;
    public float rotationSpeed = 90;
    
    [SerializeField] private float gravity = -9.8f;
    private float gravityMultiplier = 3;
    private float velocity;

    private Vector2 _input;
    private Vector3 _direction;

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        if (characterController.isGrounded) {
            velocity = -1.0f;
        } else {
            velocity += gravity * gravityMultiplier;
        }

        characterController.Move(playerSpeed * Time.deltaTime * _direction);

    }

    void ApplyRotation() {

    }

}*/
