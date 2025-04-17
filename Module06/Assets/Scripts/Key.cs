using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float _floatingSpeed;
    [SerializeField] private float _floatingSize;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Quaternion _startRot;

    void Start()
    {
        _startPos = transform.position;
        _startRot = transform.localRotation;
    }

    void Update()
    {
        transform.position = _startPos + Vector3.up * _floatingSize * Mathf.Sin(_floatingSpeed * Time.time);
        transform.rotation = _startRot * Quaternion.Euler(0, 0, _rotationSpeed * Time.time);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.KeyCollected();
            Destroy(gameObject);
        } 
    }
}
