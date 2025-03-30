using UnityEngine;

public class EnemyRangeControl : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _attackRangeMax;
    [SerializeField]
    private float _attackRangeMin;

    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _attackCooldown;

    private bool _isMoving = false;
    private bool _tooFar = false;
    private float _attackTime;

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _attackRangeMax);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRangeMin);
    }

    void Update()
    {
        if (GetComponent<EnemyHealth>().IsHit())
            return;

        var player = GameObject.FindWithTag("Player");
        var room = transform.parent.parent.gameObject;

        if (!player.GetComponent<PlayerController>().CompareCurrentRoom(room) || !player.GetComponent<PlayerController>().IsInRoom())
            return;

        _attackTime += Time.deltaTime;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        transform.up = player.transform.position;
        if (distance > _attackRangeMax)
        {
            _isMoving = true;
            _tooFar = true;
            return;
        }
        else if (distance < _attackRangeMin)
        {
            transform.up = transform.position - (player.transform.position - transform.position);
            _isMoving = true;
            _tooFar = false;
            return;
        }

        _isMoving = false;
        if (_attackTime >= _attackCooldown)
        {
            ThrowProjectile();
            _attackTime = 0;
        }
    }

    void FixedUpdate()
    {
        if (!_isMoving || GetComponent<EnemyHealth>().IsHit())
            return;

        var player = GameObject.FindWithTag("Player").transform;

        Vector3 direction = (player.position - transform.position).normalized;
        direction = _tooFar ? direction : -direction;
        transform.position += direction * _speed * Time.fixedDeltaTime;
    }

    private void ThrowProjectile()
    {
        var player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = (player.position - transform.position).normalized;

        var projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = transform.position + direction;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * _projectileSpeed;
        projectile.GetComponent<Rigidbody2D>().angularVelocity = 360;
    }
}
