using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    private Vector3 _spawnPosition;

    private Rigidbody2D _rb;
    private Vector2 _input;
    private Animator _animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _health = _maxHealth;
        _spawnPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            TakeDamage(1);
    }

    public void Spawn()
    {
        _animator.SetTrigger("Spawn");
        _health = _maxHealth;
        transform.position = _spawnPosition;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_input.x * Time.fixedDeltaTime * _playerSpeed, _rb.linearVelocity.y, 0);
        _animator.SetBool("Walking", _input.x != 0);
        // Debug.Log($"Waling: {_input.x != 0}, _input.x: {_input.x}");
    }

    public void OnJump()
    {
        // Debug.Log("Onjump");
        // Debug.Log($"IsGrounded(): {IsGrounded()}");
        if (IsGrounded()){
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            Die();
        else
            _animator.SetTrigger("TakeDamage");
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }

    public void OnMove(InputValue value)
    {
        // Debug.Log("OnMove");
        _input = value.Get<Vector2>();
    }
    
    private bool IsGrounded()
    {
        float xOffset = 0.05f;
        float yOffset = 0.05f;
        Vector2 origin = new(transform.position.x, transform.position.y);
        origin.x -= (0.5f * transform.localScale.x) - xOffset;
        origin.y -= (0.5f * transform.localScale.y) + yOffset;
        float maxDistance = transform.localScale.x - xOffset * 2f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, maxDistance, _raycastMask);
        if (hit.collider != null)
            return true;

        origin += new Vector2(0, yOffset * 2f);
        maxDistance = yOffset * 2f;
        hit = Physics2D.Raycast(origin, Vector2.down, maxDistance, _raycastMask);
        return hit.collider != null;
    }
}
