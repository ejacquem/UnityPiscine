using UnityEngine;

public class FeetCollision : MonoBehaviour
{
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.SetGrounded(true);
        Debug.Log("Jump collider was hit");
    }

    void OnTriggerExit(Collider other)
    {
        player.SetGrounded(false);
        Debug.Log("Jump collider exit");
    }
}
