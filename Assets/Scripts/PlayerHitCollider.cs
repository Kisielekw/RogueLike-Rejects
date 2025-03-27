using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().Damage(_playerData.damage);
            Debug.Log("Enemy Hit");
        }
    }
}
