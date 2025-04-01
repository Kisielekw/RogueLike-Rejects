using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private PlayerData _playerData;

    private float _hitTimer;

    void Start()
    {
    }

    public void OnHit()
    {
        transform.Find("Hit").gameObject.GetComponent<PlayerHitCollider>().OnHit();
    }

    public bool isHitting()
    {
        return _hitTimer > _playerData.HitCooldown;
    }
}
