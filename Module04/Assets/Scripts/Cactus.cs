using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    private Animator _animator;
    private GameObject _player;

    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _cooldown;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    
    private float _cooldownTimer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _cooldownTimer = 0;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        _cooldownTimer -= Time.fixedDeltaTime;
        
        if (_cooldownTimer <= 0)
        {
            TryToShoot();
        }
    }

    private void StartShoot()
    {
        _cooldownTimer = _cooldown;
        _animator.SetTrigger("Shoot");
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, quaternion.identity);
        bullet.GetComponent<Bullet>().SetDamage(_damage);
        bullet.GetComponent<Rigidbody2D>().linearVelocityX = _bulletSpeed * -transform.right.x;
    }

    private void TryToShoot()
    {
        Debug.DrawRay(transform.position, -transform.right * _range, Color.green, 5f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, _range, _raycastMask);
        if (hit.collider != null)
        {
            StartShoot();
        }
    }
}
