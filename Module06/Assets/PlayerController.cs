using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector2 _input;
    [SerializeField] private Vector2 _mouseInput;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _fps;

    [SerializeField] private CinemachineCamera _fpsCam;
    [SerializeField] private CinemachineCamera _tpsCam;
    [SerializeField] private GameObject _model;
    private SkinnedMeshRenderer _modelMesh;
    private float _pitch;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _modelMesh = _model.GetComponent<SkinnedMeshRenderer>();

        Cursor.lockState = CursorLockMode.Locked;
    
        _modelMesh.enabled = !_fps;
        _fpsCam.Priority = _fps ? 10 : 0;
        _tpsCam.Priority = _fps ? 0 : 10;
    }

    void Update()
    {
        if (_fps)
            FirstPersonController();
        else 
            ThirdPersonController();
    }

    public void OnSwitchView(InputValue value)
    {
        _fps = !_fps;
        _modelMesh.enabled = !_fps;

        _fpsCam.Priority = _fps ? 10 : 0;
        _tpsCam.Priority = _fps ? 0 : 10;

        _fpsCam.transform.localRotation = transform.localRotation;
    }

    private void FirstPersonController()
    {
        if (_mouseInput != Vector2.zero)
        {
            _fpsCam.transform.Rotate(0, _mouseInput.x * _mouseSensitivity * Time.deltaTime, 0);
            _pitch += -_mouseInput.y * _mouseSensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -80f, 80f);
            _fpsCam.transform.localRotation = Quaternion.Euler(_pitch, _fpsCam.transform.localEulerAngles.y, 0f);

            transform.rotation = Quaternion.Euler(0, _fpsCam.transform.eulerAngles.y, 0f);
        }
    }

    private void ThirdPersonController()
    {
        if (_input != Vector2.zero)
        {
            Quaternion playerRotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(_input.y, -_input.x) - 90f, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    public void OnPause(InputValue value)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();

        _animator.SetBool("Walking", _input != Vector2.zero);
    }

    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>();
    }
}
