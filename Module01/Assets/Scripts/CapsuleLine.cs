using UnityEngine;

[ExecuteInEditMode]
public class CapsuleLine : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        if (pointA == null || pointB == null) return;

        Vector3 direction = pointB.position - pointA.position;
        float distance = direction.magnitude;

        transform.position = pointA.position + direction * 0.5f + offset;

        Vector3 scale = transform.localScale;
        scale.y = distance * 0.5f;
        transform.localScale = scale;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }
}
