using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorState {OPENING, CLOSING};

    [SerializeField] private DoorState _state;
    [SerializeField] private Transform _door;

    [SerializeField] private float _speed;
    [SerializeField] private float _doorOpenAmount; // 0 to 1

    void Start()
    {
        _state = DoorState.CLOSING;
        _door = transform.GetChild(0);
    }

    void Update()
    {
        float direction = _state == DoorState.OPENING ? 1f : -1f;
        _doorOpenAmount = Mathf.Clamp01(_doorOpenAmount + direction * _speed * Time.deltaTime);
        _door.transform.localRotation = Quaternion.Euler(0, Mathf.SmoothStep(0, 90, _doorOpenAmount), 0);
    }

    void OnTriggerEnter(Collider other)
    {
        _state = DoorState.OPENING;
        Debug.Log("Door OnTrigger");
    }

    void OnTriggerExit(Collider other)
    {
        _state = DoorState.CLOSING;
    }
}
