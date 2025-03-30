using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMeleeControl : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private float _attackCooldown;

    [SerializeField]
    private int _attackDamage;

    private bool _isMoving = false;
    private float _attackTime;

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyHealth>().IsHit())
            return;

        var player = GameObject.FindWithTag("Player");
        var room = transform.parent.parent.gameObject;

        if(!player.GetComponent<PlayerController>().CompareCurrentRoom(room) || !player.GetComponent<PlayerController>().IsInRoom())
            return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        transform.up = (player.transform.position - transform.position);
        if (distance > _attackRange)
        {
            _isMoving = true;
            _attackTime = _attackCooldown;
            return;
        }

        _isMoving = false;
        _attackTime += Time.deltaTime;
        if (_attackTime >= _attackCooldown)
        {
            player.GetComponent<PlayerHelthScript>().TakeDamage(_attackDamage);
            _attackTime = 0;
        }
    }

    void FixedUpdate()
    {
        if (!_isMoving || GetComponent<EnemyHealth>().IsHit())
            return;

        var player = GameObject.FindWithTag("Player").transform;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * _speed * Time.fixedDeltaTime;
    }

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
