using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public enum RoomDirection
    {
        up,
        down,
        left,
        right
    }

    public bool Up, Down, Left, Right;
    
    private int roomDepth;
    private GameObject upRoom, downRoom, leftRoom, rightRoom;

    public void SetRoomDepth(int depth)
    {
        roomDepth = depth;
    }

    public int GetDoorNumber()
    {
        int doorNumber = 0;
        if (Up) doorNumber++;
        if (Down) doorNumber++;
        if (Left) doorNumber++;
        if (Right) doorNumber++;
        return doorNumber;
    }

    public void SetRoomPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public Vector2 GetRoomPosition()
    {
        return transform.position;
    }

    public void SetRoomDirection(RoomDirection direction, GameObject room)
    {
        switch (direction)
        {
            case RoomDirection.up:
                upRoom = room;
                break;
            case RoomDirection.down:
                downRoom = room;
                break;
            case RoomDirection.left:
                leftRoom = room;
                break;
            case RoomDirection.right:
                rightRoom = room;
                break;
        }
    }

    public int getRoomDepth()
    {
        return roomDepth;
    }

    public bool HasRoomInDirection(RoomDirection direction)
    {
        switch (direction)
        {
            case RoomDirection.up:
                return upRoom != null;
            case RoomDirection.down:
                return downRoom != null;
            case RoomDirection.left:
                return leftRoom != null;
            case RoomDirection.right:
                return rightRoom != null;
            default:
                return false;
        }
    }
}
