using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _timeToSpawn;
    [SerializeField]
    private float _timeToSpawnMult;
    [SerializeField]
    private float _startTime;
    private float _spawnTimer;
    public bool _isActive;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private int _enemyToSpawn;
    private int _enemySpawned = 0;

    void Start()
    {
        _isActive = true;
        _spawnTimer = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isActive || IsEmpty())
            return;
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0){
            _timeToSpawn *= _timeToSpawnMult;
            _spawnTimer = _timeToSpawn;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _enemySpawned++;
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }

    // return if the spawner has no more enemy to spawn
    public bool IsEmpty()
    {
        return _enemySpawned >= _enemyToSpawn;
    }

    public int GetEnemyToSpawn()
    {
        return _enemyToSpawn;
    }
}
