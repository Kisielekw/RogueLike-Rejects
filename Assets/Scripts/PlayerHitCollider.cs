using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private float _hitSize;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _hitSize);
    }

    public void OnHit()
    {
        var objectsHit = Physics2D.OverlapCircleAll(transform.position, _hitSize);
        foreach (Collider2D obj in objectsHit)
        {
            if (!obj.CompareTag("Enemy"))
                continue;

            obj.GetComponent<EnemyHealth>().Damage(_playerData.damage);
        }
    }
}
