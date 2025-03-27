using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    private Camera _camera;

    private InputAction _movement;
    private InputAction _look;
    private InputAction _hit;

    private GameObject _weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;

        _weapon = transform.Find("Weapon").gameObject;

        _movement = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _hit = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerScreenPos = _camera.WorldToScreenPoint(transform.position);
        Vector2 LookVector = (_look.ReadValue<Vector2>() - playerScreenPos).normalized;

        _weapon.transform.up = LookVector;
        _weapon.transform.localPosition = new Vector3(LookVector.x, LookVector.y, 0);

        
        if(_hit.IsPressed())
            _weapon.GetComponent<PlayerAttack>().OnHit();

    }

    void FixedUpdate()
    {
        Vector2 movement = _movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(movement.x , movement.y, 0) * _playerData.speed * Time.fixedDeltaTime);
    }
}
