using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private PlayerData _playerData;

    private float _hitTimer;

    void Start()
    {
        _hitTimer = _playerData.HitCooldown;
    }

    void Update()
    {
        if (_hitTimer >= _playerData.HitCooldown)
            return;

        _hitTimer += Time.deltaTime;
    }

    public void OnHit()
    {
        if(_hitTimer < _playerData.HitCooldown)
            return;

        transform.Find("Hit").gameObject.GetComponent<PlayerHitCollider>().OnHit();
        _hitTimer = 0.0f;
    }

    public bool isHitting()
    {
        return _hitTimer > _playerData.HitCooldown;
    }
}
