using UnityEngine;

public class Vine : MonoBehaviour
{
    private Animator _animator;
    private GameObject _player;
    [SerializeField] private float _damage;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_animator.GetBool("Attacking"))
        {
            // Debug.Log($"Vine collided with {collision.name}");
            Attack();
            _player = collision.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = null;
        }
    }

    private void Attack()
    {
        _animator.SetBool("Attacking", true);
        // Debug.Log($"Attack");
    }

    public void AttackDone()
    {
        _animator.SetBool("Attacking", false);
    }

    public void DamagePlayer()
    {
        AudioManager.Instance.Play("VineAttack");
        if (_player)
        {
            _player.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}
