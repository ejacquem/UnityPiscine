using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float playerJumpForce;
    public float maxSpeed;
    public float gravity;
    public float downGravityMult;

    private Vector2 _input;
    private Rigidbody _rb;
    private bool _canJump = false;
    private bool _isGrounded = false;
    private float _isGroundedTimer = 0f;
    private float _noJumpTimer = 0f;

    public const float GroundedTime = 0.075f;
    public const float NoJumpTime = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _noJumpTimer -= Time.deltaTime;
        if (_isGrounded)
            _canJump = true;
        else {
            _isGroundedTimer -= Time.deltaTime;
            if(_isGroundedTimer <= 0f)
                _canJump = false;
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_input.x * Time.fixedDeltaTime * playerSpeed, _rb.linearVelocity.y, 0);
        if (_rb.linearVelocity.y < 0)
            _rb.AddForce(new Vector3(0,Time.fixedDeltaTime * gravity * downGravityMult,0));
        else
            _rb.AddForce(new Vector3(0,Time.fixedDeltaTime * gravity,0));
    }

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;
        if(!grounded)
            _isGroundedTimer = GroundedTime;
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
    }

    private void OnMove(InputValue value)
    {
        // Debug.Log("Onmove called");
        _input = value.Get<Vector2>();
        if (_input.y > 0)
            OnJump();
    }

    private void OnJump()
    {
        Debug.Log("Onjump called, _isGrounded: " + _isGrounded.ToString() + ", canJump: " + _canJump.ToString());
        if (_canJump && _noJumpTimer <= 0f){
            _noJumpTimer = NoJumpTime;
            _rb.AddForce(playerJumpForce * transform.up);
        }
    }
}
