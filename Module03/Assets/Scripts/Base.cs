using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float _health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log($"Base took damage ! Health: {_health}");
        // if(_health <= 0)
        //     Destroy(gameObject);
    }

    public float GetHealth()
    {
        return _health;
    }
}
