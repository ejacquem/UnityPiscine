using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private float _hoverSpeed;
    [SerializeField] private float _hoverSize;
    private Vector3 startPos;
    private Vector3 pos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        pos = startPos;
        pos.y += Mathf.Sin(Time.time * _hoverSpeed) * _hoverSize;
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Leaf Collected");
            GameManager.Instance.AddPoints(_points);
            Destroy(gameObject);
        }
    }
}
