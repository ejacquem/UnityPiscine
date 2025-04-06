using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletLifeTime;

    private float shootTimer;
    private Color _turretColor;

    void Start()
    {
        _turretColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0){
            shootTimer = 1f/fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
        instance.GetComponent<MeshRenderer>().material.color = _turretColor;
        instance.GetComponent<Bullet>().Spawn(bulletSpeed, cannon.transform.up, bulletLifeTime);
    }
}
