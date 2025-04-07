using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;

    private Vector3 _position;

    void Start()
    {
        _position = transform.position;
        Destroy(gameObject, 300f);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _position.y -= _speed * Time.deltaTime;
        transform.position = _position;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Base")){
            Base b = collision.gameObject.GetComponent<Base>();
            b.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
