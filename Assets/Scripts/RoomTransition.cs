using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField]
    private Vector2 _direction;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.Transition(_direction * new Vector2(2, 2) + new Vector2(transform.position.x, transform.position.y));

            var camera = Camera.main.gameObject.GetComponent<CameraTransition>();
            camera.Transition(new Vector3(transform.position.x, transform.position.y, -10) + (Vector3)(_direction * new Vector2(9, 5)));

        }
    }
}
