using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private PlayerData _playerData;

    private float _hitTimer;
    private bool _isHitting;

    void Start()
    {
        _hitTimer = _playerData.hitCooldown;
    }

    void Update()
    {
        if (_hitTimer >= _playerData.hitCooldown)
            return;

        _hitTimer += Time.deltaTime;
        if (_hitTimer >= _playerData.hitTime && _isHitting)
            HitReset();
    }

    public void OnHit()
    {
        if(_hitTimer < _playerData.hitCooldown)
            return;

        GameObject weaponHitBox = transform.Find("Hit").gameObject;
        weaponHitBox.SetActive(true);
        _hitTimer = 0.0f;
        _isHitting = true;
    }

    void HitReset()
    {
        GameObject weaponHitBox = transform.Find("Hit").gameObject;
        weaponHitBox.SetActive(false);
        _isHitting = false;
    }
}
