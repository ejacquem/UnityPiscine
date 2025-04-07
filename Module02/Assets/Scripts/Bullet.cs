using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _basicDamage;
    private float _damage;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void DamageMult(float damage)
    {
        _damage = _basicDamage * damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

}
