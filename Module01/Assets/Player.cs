using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float playerJumpForce;
    public float maxSpeed;

    private Vector2 _input;
    private Rigidbody _rb;
    private bool _isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += Time.deltaTime * playerSpeed * new Vector3(_input.x,0,0);
    }

    void FixedUpdate()
    {
        _rb.AddForce(new Vector3(_input.x * Time.deltaTime * playerSpeed,0,0));

        _rb.linearVelocity = new Vector3(Math.Clamp(_rb.linearVelocity.x, -maxSpeed, maxSpeed), _rb.linearVelocity.y, 0);
    }

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
    }

    private void OnMove(InputValue value)
    {
        Debug.Log("Onmove called");
        _input = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("Onjump called, _isGrounded: " + _isGrounded.ToString());
        if (_isGrounded)
            _rb.AddForce(playerJumpForce * transform.up);
    }
}
