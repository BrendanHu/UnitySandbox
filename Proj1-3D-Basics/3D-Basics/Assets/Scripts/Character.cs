using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour {
    private Animator _animator;
    private Collider _collider;
    private Rigidbody _rb;
    private Vector3 _respawnPoint;

    [Header("Movement Variables")]
    public float horizontal;
    public float vertical;
    public float maxMoveSpeed = 1;
    public float turnSpeed = 30;
    public float runAcceleration;
    public float runDeceleration;
    public float friction;
    [SerializeField] private bool _canMove = true;

    float xVel;
    float yVel;
    public Vector3 movement;
    public float veldebug;

    // Called before first frame update
    private void Start() {
        _rb = this.GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();

        // Set respawn point to initial position
        SetRespawnPoint(transform.position);
    }
    // Called once per frame
    private void Update() {
        if (!_canMove) {
            horizontal = 0;
            vertical = 0;
            return;
        }
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


        float hSpeedDiff = targetHSpeed - _rb.velocity.x;
        float vSpeedDiff = targetVSpeed - _rb.velocity.z;
        /*print("h"); print(hSpeedDiff);
        print("v"); print(vSpeedDiff);*/

        float hMovement = hSpeedDiff * hAccelRate;
        float vMovement = vSpeedDiff * vAccelRate;
        /*print(hMovement);
        print(vMovement);*/

        #region Friction
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            float hAmount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(friction));
            hAmount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -hAmount, ForceMode.Impulse);
        }
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) {
            float vAmount = Mathf.Min(Mathf.Abs(_rb.velocity.z), Mathf.Abs(friction));
            vAmount *= Mathf.Sign(_rb.velocity.z);
            _rb.AddForce(Vector3.forward * -vAmount, ForceMode.Impulse);
        }
        #endregion
        
        _rb.AddForce(hMovement * Vector3.right, ForceMode.Force);
        _rb.AddForce(vMovement * Vector3.forward, ForceMode.Force);

        // For animator on movement update
        _animator.SetFloat("speed", _rb.velocity.magnitude);
        veldebug = _rb.velocity.magnitude;

        if (_rb.position.y < -50f) {
            Die();
        }

        /*// Cap the move speed
        if (rb.velocity.magnitude > maxMoveSpeed) {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }*/
    }

    #region Dying!
    private void SetRespawnPoint(Vector3 point) {
        _respawnPoint = point;
    }

    private void MiniJump() {
        //_rb.velocity = new Vector3(_rb.velocity.x, 100f, _rb.velocity.z);
        _rb.AddForce(8f * Vector3.up, ForceMode.Impulse);
    }
    public void Die() {
        _canMove = false;
        _collider.enabled = false;
        MiniJump();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds(1f);
        _rb.velocity = new Vector3(0f, 0f, 0f);
        transform.position = _respawnPoint;
        _canMove = true;
        _collider.enabled = true;
    }
    #endregion
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
