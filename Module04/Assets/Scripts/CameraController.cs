using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _yBound;
    [SerializeField] private float _yOffset;

    void Start()
    {
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 pos = transform.position;
        pos.y = _yOffset + Mathf.Clamp(_player.transform.position.y, _yBound.x, _yBound.y);
        pos.x = _player.transform.position.x;
        transform.position = pos;
    }

    public void OnValidate()
    {
        FollowPlayer();
    }
}
