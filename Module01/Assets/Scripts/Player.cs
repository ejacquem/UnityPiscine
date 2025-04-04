using System;
using System.Linq.Expressions;
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
    private float _isGroundedTimer = 0f;

    public const float GroundedTime = 0.075f;
    public const float NoJumpTime = 0.1f;

    [SerializeField]
    private LayerMask raycastMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfGrounded()){
            _canJump = true;
            _isGroundedTimer = GroundedTime;
        }
        else {
            _isGroundedTimer -= Time.deltaTime;
            if(_isGroundedTimer <= 0f)
                _canJump = false;
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_input.x * Time.fixedDeltaTime * playerSpeed, _rb.linearVelocity.y, 0);

        // apply gravity
        float gravityMult = _rb.linearVelocity.y < 0 ? downGravityMult : 1f; 
        _rb.AddForce(Time.fixedDeltaTime * gravity * _rb.mass * gravityMult * Vector3.down);
    }

    public void SetInput(Vector2 input)
    {
        _input = input;
    }

    private bool CheckIfGrounded()
    {
        float xOffset = 0.05f;
        float yOffset = 0.05f;
        Vector3 origin = transform.position + new Vector3((-0.5f * transform.localScale.x) + xOffset, (-0.5f * transform.localScale.y) - yOffset,0);
        float maxDistance = transform.localScale.x - xOffset * 2f;

        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.right, out hit, maxDistance, raycastMask))
        {
            // Debug.Log("Hit: " + hit.collider.name);
            // Debug.DrawRay(origin, Vector3.right * hit.distance, Color.red, 5f);
            return true;
        }
        else
        {
            // Debug.DrawRay(origin, Vector3.right * maxDistance, Color.green, 5f);

            origin += new Vector3(0, yOffset * 2f,0);
            maxDistance = 0.1f;
            if (Physics.Raycast(origin, Vector3.down, out hit, maxDistance, raycastMask))
            {
                // Debug.Log("Hit: " + hit.collider.name);
                // Debug.DrawRay(origin, Vector3.down * hit.distance, Color.red, 5f);
                return true;
            }
            else
            {
                // Debug.DrawRay(origin, Vector3.down * maxDistance, Color.green, 5f);
                return false;
            }
        }
        
    }

    public void Jump()
    {
        if(_canJump)
            _rb.AddForce(playerJumpForce * transform.up);
        Debug.Log("player jump -------------------------------");
    }

}
