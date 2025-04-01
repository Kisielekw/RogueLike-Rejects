using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private float _hitSize;

    [SerializeField]
    private LayerMask _enemyLayer;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _hitSize);
    }

    public void OnHit()
    {
        var objectsHit = Physics2D.OverlapCircleAll(transform.position, _hitSize, _enemyLayer);
        foreach (Collider2D obj in objectsHit)
        {
            obj.GetComponent<EnemyHealth>().Damage(_playerData.Damage);
        }
    }
}
