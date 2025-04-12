using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        _spawnPosition = GameObject.FindGameObjectWithTag("PortalStart").transform.position - Vector3.up * 2.5f;
        transform.position = new Vector3(
            PlayerPrefs.GetFloat("PositionX", _spawnPosition.x),
            PlayerPrefs.GetFloat("PositionY", _spawnPosition.y),
            0);
        SetHealth(PlayerPrefs.GetFloat("Health", _maxHealth));
        PlayerPrefs.SetInt("Stage", SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            TakeDamage(1);
    }

    public void Spawn()
    {
        AudioManager.Instance.Play("Spawn");
        _animator.SetTrigger("Spawn");
        SetHealth(_maxHealth);
        transform.SetPositionAndRotation(_spawnPosition, Quaternion.Euler(0 ,0, 0));
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
        if (IsGrounded() && _health > 0){
            AudioManager.Instance.Play("Jump");
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    public void TakeDamage(float damage)
    {
        SetHealth(_health - damage);
        if (_health <= 0)
            Die();
        else
        {
            AudioManager.Instance.Play("Hit");
            _animator.SetTrigger("TakeDamage");
        }
    }

    private void SetHealth(float health)
    {
        _health = Mathf.Max(0, health);
        PlayerPrefs.SetFloat("Health", _health);
        UIManager.Instance.DisplayPlayerHealth(_health);
    }

    public void Die()
    {
        AudioManager.Instance.Play("Death");
        GameManager.Instance.AddDeaths();
        _animator.SetTrigger("Die");
    }

    public void OnMove(InputValue value)
    {
        // Debug.Log("OnMove");
        _input = value.Get<Vector2>();
        if (_input.x > 0)
            transform.rotation = Quaternion.Euler(0 ,0, 0);
        else if (_input.x < 0)
            transform.rotation = Quaternion.Euler(0 ,180, 0);
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
