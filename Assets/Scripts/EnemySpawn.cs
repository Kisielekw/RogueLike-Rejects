using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLightPrefab;
    [SerializeField] private GameObject _enemyHeavyPrefab;
    [SerializeField] private GameObject _enemyRangedPrefab;
    [SerializeField] private GameObject _enemyBossPrefab;

    [SerializeField] private Vector2[] _lightSpawnPos;
    [SerializeField] private Vector2[] _heavySpawnPos;
    [SerializeField] private Vector2[] _rangedSpawnPos;

    [SerializeField] private int _lightNumber;
    [SerializeField] private int _heavyNumber;
    [SerializeField] private int _rangedNumber;

    [SerializeField] private EclipseData _eclipseData;

    [SerializeField] private bool _isBossRoom;

    private bool _isEclipseActive = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach (var pos in _lightSpawnPos)
        {
            Gizmos.DrawWireSphere(pos, 0.5f);
        }

        Gizmos.color = Color.green;
        foreach (var pos in _heavySpawnPos)
        {
            Gizmos.DrawWireSphere(pos, 0.5f);
        }

        Gizmos.color = Color.red;
        foreach (var pos in _rangedSpawnPos)
        {
            Gizmos.DrawWireSphere(pos, 0.5f);
        }

        if (!_isBossRoom)
            return;

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
    }

    void Start()
    {
        SpawnEnemiesLight();
        SpawnEnemiesHeavy();
        SpawnEnemiesRanged();
        if(_isBossRoom)
            SpawnEnemiesBoss();
    }

    void FixedUpdate()
    {
        if (_eclipseData.IsEclipseActive && transform.childCount > 0 && !_isEclipseActive)
        {
            _isEclipseActive = true;
            SpawnEnemiesLight();
        }
        else if (_isEclipseActive && !_eclipseData.IsEclipseActive)
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
            var displacement = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            enemy.transform.localPosition += (Vector3)displacement;
        }
    }

    void SpawnEnemiesHeavy()
    {
        for (int i = 0; i < _heavyNumber; i++)
        {
            var enemy = Instantiate(_enemyHeavyPrefab, transform);
            enemy.transform.localPosition = _heavySpawnPos[Random.Range(0, _heavySpawnPos.Length)];
            var displacement = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            enemy.transform.localPosition += (Vector3)displacement;

        }
    }

    void SpawnEnemiesRanged()
    {
        for (int i = 0; i < _rangedNumber; i++)
        {
            var enemy = Instantiate(_enemyRangedPrefab, transform);
            enemy.transform.localPosition = _rangedSpawnPos[Random.Range(0, _rangedSpawnPos.Length)];
            var displacement = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            enemy.transform.localPosition += (Vector3)displacement;
        }
    }

    void SpawnEnemiesBoss()
    {
        var enemy = Instantiate(_enemyBossPrefab, transform);
        enemy.transform.localPosition = Vector3.zero;
    }
}
