using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(z * Time.deltaTime, x * Time.deltaTime, y * Time.deltaTime));
    }
}
