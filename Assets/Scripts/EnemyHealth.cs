using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private float _backForce;

    private Vector2 _hitStart;
    private Vector2 _hitEnd;

    private float _hitTimer;
    private bool _isHit;

    void Update()
    {
        if(!_isHit)
            return;

        _hitTimer += Time.deltaTime;

        transform.position = Vector2.Lerp(_hitStart, _hitEnd, _hitTimer * 4f);

        if (_hitTimer >= 0.25f)
        {
            _hitTimer = 0;
            _isHit = false;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void Damage(int damage)
    {
        if (_isHit)
            return;

        GetComponent<Collider2D>().enabled = false;

        _isHit = true;
        _health -= damage;

        if(_health <= 0)
            Destroy(gameObject, 0.25f);

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
