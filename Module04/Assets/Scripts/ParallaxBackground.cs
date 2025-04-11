using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Material _mat;
    private Vector2 _dist;
    private Vector2 _startPos;
    [SerializeField] private float _parallaxPower;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _startPos = transform.position;
    }

    void Update()
    {
        Vector2 _dist = new Vector2(transform.position.x, transform.position.y) - _startPos;
        _mat.SetTextureOffset("_MainTex", _dist * _parallaxPower);
    }
}
