using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;

    void Awake()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Cactus hit {collision.name}");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
