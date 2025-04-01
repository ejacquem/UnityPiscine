using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 _input;
    private Rigidbody _rb;
    
    private bool isGrounded;
    
    public float maxSpeed = 10f;
    public float playerSpeed;
    public float playerJumpForce;
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        // gameObject.transform.position += _input.y * playerSpeed * Time.deltaTime * transform.forward;
        // gameObject.transform.position += _input.x * playerSpeed * Time.deltaTime * transform.right;
        
    }
    
    void FixedUpdate()
    {
        _rb.AddForce(_input.y * playerSpeed * Time.deltaTime * transform.forward);
        _rb.AddForce(_input.x * playerSpeed * Time.deltaTime * transform.right);

        var xyVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y);
        if (xyVelocity.magnitude > maxSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (isGrounded)
            _rb.AddForce(playerJumpForce * transform.up);
        Debug.Log("OnJump Called");
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
