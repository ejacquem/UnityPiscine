using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    public float _speed;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * _speed);
    }
}
