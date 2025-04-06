using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private Vector3 _dir;

    void Start()
    {
    }

    void Update()
    {
        transform.position += _speed * Time.deltaTime * _dir;
    }

    public void Spawn(float speed, Vector3 direction, float lifeTime)
    {
        _speed = speed;
        _dir = Vector3.Normalize(direction);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit {other.gameObject.name}");
        if (gameObject.GetComponent<MeshRenderer>().material.color == other.gameObject.GetComponent<MeshRenderer>().material.color){
            Player player = other.gameObject.GetComponent<Player>();
            if(player){
                player.Kill();
            }
        }
        Destroy(gameObject);
    }

}
