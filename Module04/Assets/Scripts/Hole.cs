using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player fell in {name}");
            collision.GetComponent<PlayerController>().TakeDamage(1000f);
        }
    }
}
