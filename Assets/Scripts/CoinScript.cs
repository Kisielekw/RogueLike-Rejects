using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _playerData.Score++;
            Destroy(gameObject);
        }
    }
}
