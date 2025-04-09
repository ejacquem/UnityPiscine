using UnityEngine;

public class Liana : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Liana collided with {collision.name}");
        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        Debug.Log($"Attack");
    }

    public void Func()
    {
        Debug.Log("Bim !");
    }
}
