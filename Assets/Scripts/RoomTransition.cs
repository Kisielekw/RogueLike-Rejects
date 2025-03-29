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
            player.Transition(_direction * new Vector2(2, 2) + new Vector2(transform.position.x, transform.position.y), getRoomInDirection(_direction));

            var camera = Camera.main.gameObject.GetComponent<CameraTransition>();
            var parentPos = transform.parent.position;
            camera.Transition(new Vector3(parentPos.x, parentPos.y, -10) + (Vector3)(_direction * new Vector2(18, 10)));
        }
    }

    GameObject getRoomInDirection(Vector2 direction)
    {
        var roomInfo = GetComponentInParent<RoomInfo>();
        if (direction == Vector2.up)
            return roomInfo.GetRoomInDirection(RoomInfo.RoomDirection.up);
        if (direction == Vector2.down)
            return roomInfo.GetRoomInDirection(RoomInfo.RoomDirection.down);
        if (direction == Vector2.left)
            return roomInfo.GetRoomInDirection(RoomInfo.RoomDirection.left);
        if (direction == Vector2.right)
            return roomInfo.GetRoomInDirection(RoomInfo.RoomDirection.right);
        return null;
    }
}
