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

    private bool _transitioning = false;
    private float _transitionTime = 0;
    private Vector2 _transitionDestination;
    private Vector2 _transitionStart;

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

        
        if(_hit.IsPressed() && !_transitioning)
            _weapon.GetComponent<PlayerAttack>().OnHit();

    }

    void FixedUpdate()
    {
        if(_transitioning)
        {
            _transitionTime += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(_transitionStart, _transitionDestination, _transitionTime);
            if(Vector3.Distance(transform.position, _transitionDestination) < 0.1f)
            {
                _transitioning = false;
            }
            return;
        }
        Vector2 movement = _movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(movement.x , movement.y, 0) * _playerData.speed * Time.fixedDeltaTime);
    }

    public void Transition(Vector2 direction)
    {
        if (!_transitioning)
        {
            _transitioning = true;
            _transitionDestination = direction;
            _transitionStart = transform.position;
            _transitionTime = 0;
        }
    }
}
