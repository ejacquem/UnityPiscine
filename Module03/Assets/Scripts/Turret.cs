using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float _fireRate;
    private float _fireTimer;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _price;

    private List<GameObject> _enemyList;
    private Transform _currentTarget;

    [SerializeField]
    private float _dps;
    [SerializeField]
    private float _dpsPerCost;

    void Start()
    {
        _enemyList = new List<GameObject>();
        if (transform.position.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public float GetDamage(){return _damage;}
    public float GetFireRate(){return _fireRate;}
    public float GetPrice(){return _price;}


    void Update()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0 && _enemyList.Count > 0)
        {
            // Debug.Log("Fire !");
            Fire();
            _fireTimer = 1f / _fireRate;
        }
    }

    private bool Compare2DDist(Vector3 A, Vector3 B)
    {
        return A.x*A.x+A.y*A.y < B.x*B.x+B.y*B.y;
    }

    private void Fire()
    {
        // Find closest enemy
        GameObject closestEnemy;
        closestEnemy = _enemyList[0];
        foreach (GameObject enemy in _enemyList)
        {
            if (enemy == null)
                continue;
            if (closestEnemy == null)
                closestEnemy = enemy;
            if (Compare2DDist(enemy.transform.position - transform.position, closestEnemy.transform.position - transform.position))
                closestEnemy = enemy;
        }

        if (closestEnemy == null)
            return;
        // Shoot bullet
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().DamageMult(_damage);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.Normalize(closestEnemy.transform.position - transform.position) * _bulletSpeed;

        _currentTarget = closestEnemy.transform;
    }

    void OnDrawGizmos()
    {
        if(_currentTarget != null){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _currentTarget.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log($"Turret Collided");
        if (collision.gameObject.CompareTag("Enemy")){
            // Debug.Log($"Enemy entered Turret radius");
            _enemyList.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log($"Enemy exit Turret radius");
        if (collision.gameObject.CompareTag("Enemy")){
            _enemyList.Remove(collision.gameObject);
        }
    }

    private void OnValidate()
    {
        _dps = _damage * _fireRate;
        _dpsPerCost = _dps / _price;
    }
}
