using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLightPrefab;
    [SerializeField] private GameObject _enemyHeavyPrefab;
    [SerializeField] private GameObject _enemyRangedPrefab;

    [SerializeField] private Vector2[] _lightSpawnPos;
    [SerializeField] private Vector2[] _heavySpawnPos;
    [SerializeField] private Vector2[] _rangedSpawnPos;

    [SerializeField] private int _lightNumber;
    [SerializeField] private int _heavyNumber;
    [SerializeField] private int _rangedNumber;

    [SerializeField] private EclipseData _eclipseData;

    private bool _isEclipseActive = false;

    void Start()
    {
        SpawnEnemiesLight();
        SpawnEnemiesHeavy();
        SpawnEnemiesRanged();
    }

    void FixedUpdate()
    {
        if (_eclipseData.IsEclipseActive && transform.childCount > 0 && ! _isEclipseActive)
        {
            _isEclipseActive = true;
            SpawnEnemiesLight();
        }
        else if(_isEclipseActive && !_eclipseData.IsEclipseActive)
        {
            _isEclipseActive = false;
        }
    }

    void SpawnEnemiesLight()
    {
        for (int i = 0; i < _lightNumber; i++)
        {
            var enemy = Instantiate(_enemyLightPrefab, transform);
            enemy.transform.localPosition = _lightSpawnPos[Random.Range(0, _lightSpawnPos.Length)];
        }
    }

    void SpawnEnemiesHeavy()
    {
        for (int i = 0; i < _heavyNumber; i++)
        {
            var enemy = Instantiate(_enemyHeavyPrefab, transform);
            enemy.transform.localPosition = _heavySpawnPos[Random.Range(0, _heavySpawnPos.Length)];
        }
    }

    void SpawnEnemiesRanged()
    {
        for (int i = 0; i < _rangedNumber; i++)
        {
            var enemy = Instantiate(_enemyRangedPrefab, transform);
            enemy.transform.localPosition = _rangedSpawnPos[Random.Range(0, _rangedSpawnPos.Length)];
        }
    }
}
