using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _yBound;
    [SerializeField] private float _yOffset;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_player == null)
            return;
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
