using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    private Camera _camera;

    private InputAction _movement;
    private InputAction _look;
    private InputAction _hit;
    private InputAction _exit;

    private GameObject _weapon;

    private bool _transitioning = false;
    private float _transitionTime = 0;
    private float _hitTimer = 0;
    private Vector2 _transitionDestination;
    private Vector2 _transitionStart;

    private GameObject _currentRoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;

        _weapon = transform.Find("Weapon").gameObject;

        _movement = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _hit = InputSystem.actions.FindAction("Attack");
        _exit = InputSystem.actions.FindAction("Exit");

        _hitTimer = _playerData.HitCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        _hitTimer += Time.deltaTime;

        if (_exit.IsPressed())
            SceneManager.LoadScene(0);

        if (_playerData.Health <= 0)
            return;

        Vector2 playerScreenPos = _camera.WorldToScreenPoint(transform.position);
        Vector2 LookVector = (_look.ReadValue<Vector2>() - playerScreenPos).normalized;

        _weapon.transform.up = LookVector;
        _weapon.transform.localPosition = (Vector3) LookVector;

        _weapon.SetActive(false);
        if (_hitTimer < _playerData.HitCooldown)
            return;

        _weapon.SetActive(true);

        if (_hit.IsPressed() && !_transitioning)
        {
            _hitTimer = 0.0f;
            _weapon.GetComponent<PlayerAttack>().OnHit();
        }
    }

    void FixedUpdate()
    {
        if (_playerData.Health <= 0)
            return;

        if(_transitioning)
        {
            _transitionTime += Time.fixedDeltaTime;
            _transitionTime = Mathf.Clamp(_transitionTime, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(_transitionStart, _transitionDestination, _transitionTime);
            if(Mathf.Abs(_transitionTime - 1.0f) < 0.0001)
                _transitioning = false;
            return;
        }
        Vector2 movement = _movement.ReadValue<Vector2>();
        transform.Translate(new Vector3(movement.x , movement.y, 0).normalized * _playerData.speed * Time.fixedDeltaTime);
    }

    public void Transition(Vector2 direction, GameObject room)
    {
        if (!_transitioning)
        {
            _transitioning = true;
            _transitionDestination = direction;
            _transitionStart = transform.position;
            _transitionTime = 0;

            _currentRoom = room;
        }
    }

    public bool CompareCurrentRoom(GameObject room)
    {
        return room == _currentRoom;
    }

    public void SetCurrentRoom(GameObject room)
    {
        _currentRoom = room;
    }

    public bool IsInRoom()
    {
        return !_transitioning;
    }
}