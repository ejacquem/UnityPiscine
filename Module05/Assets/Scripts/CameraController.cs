using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _yBound;
    [SerializeField] private float _yOffset;
    [SerializeField] private bool _followMouse = false;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if(_followMouse)
            FollowPos(new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height) * 2f);
        else if (_player)
            FollowPos(_player.transform.position);
    }

    private void FollowPos(Vector3 pos)
    {
        Vector3 p = transform.position;
        p.y = _yOffset + Mathf.Clamp(pos.y, _yBound.x, _yBound.y);
        p.x = pos.x;
        transform.position = p;
    }

    public void OnValidate()
    {
        if (_player)
            FollowPos(_player.transform.position);
    }
}
