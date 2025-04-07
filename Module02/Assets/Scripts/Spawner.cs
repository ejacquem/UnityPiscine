using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _timeToSpawn;
    [SerializeField]
    private float _startTime;
    private float _spawnTimer;
    public bool _isActive;

    [SerializeField]
    private GameObject _enemyPrefab;

    void Start()
    {
        _isActive = true;
        _spawnTimer = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isActive)
            return;
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0){
            _spawnTimer = _timeToSpawn;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
