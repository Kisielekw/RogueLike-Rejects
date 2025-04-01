using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private int _healthMax;

    [SerializeField]
    private float _backForce;

    [SerializeField]
    private EclipseData _eclipseData;

    [SerializeField]
    private GameObject _coin;

    [SerializeField]
    private int _coinDrop;

    private Vector2 _hitStart;
    private Vector2 _hitEnd;

    private float _hitTimer;
    private bool _isHit;
    private bool _isEclipseActive;

    void Update()
    {
        if (_eclipseData.IsEclipseActive && !_isEclipseActive)
        {
            _isEclipseActive = true;

            if (CompareTag("HeavyEnemy"))
                _health += _healthMax;
        }

        if(!_eclipseData.IsEclipseActive && _isEclipseActive)
        {
            _isEclipseActive = false;
            if (CompareTag("HeavyEnemy"))
                _health -= _healthMax;
        }

        if (!_isHit)
            return;

        _hitTimer += Time.deltaTime;

        transform.position = Vector2.Lerp(_hitStart, _hitEnd, _hitTimer * 4f);

        if (_hitTimer >= 0.25f)
        {
            _hitTimer = 0;
            _isHit = false;
        }
    }

    public void Damage(int damage)
    {
        if (_isHit)
            return;

        _isHit = true;
        _health -= damage;

        if(_health <= 0)
        {
            for(int i = 0; i < _coinDrop; i++)
            {
                var coin = Instantiate(_coin, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * 100);
                coin.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-1f, 1f) * 100);
            }

            Destroy(gameObject, 0.25f);
        }

        var player = GameObject.FindWithTag("Player");
        Vector2 playerToEnemy = (transform.position - player.transform.position).normalized;
        _hitStart = transform.position;
        _hitEnd = (Vector2)transform.position + (playerToEnemy * _backForce);
    }

    public bool IsHit()
    {
        return _isHit;
    }
}
