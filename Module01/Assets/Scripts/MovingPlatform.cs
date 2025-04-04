using UnityEngine;
using UnityEditor;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    [SerializeField]
    private Transform platform;

    [SerializeField]
    private float speed;

    private float dist = 0f;
    private Vector3 direction;

    void Start()
    {
        
    }

    void Update()
    {
        direction = pointB.position - pointA.position;
        dist = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        platform.position = pointA.position + direction * dist;
    }
}
