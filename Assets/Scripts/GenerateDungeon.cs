using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    [SerializeField]
    private int _dungeonDepth;

    [SerializeField]
    private GameObject[] _roomPrefabs;

    [SerializeField]
    private GameObject[] _entryPrefab;

    private List<GameObject> _roomList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _roomList = new List<GameObject> { Instantiate(_entryPrefab[Random.Range(0,_entryPrefab.Length-1)], transform) };
        var roomInfo = _roomList[0].GetComponent<RoomInfo>();
        roomInfo.SetRoomDepth(0);
        roomInfo.SetRoomPosition(Vector2.zero);

        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.SetCurrentRoom(_roomList[0]);

        GenerateRooms();
    }

    void GenerateRooms()
    {
        var availableRooms = _roomPrefabs;

        var room = _roomList[^1];
        var roomInfo = room.GetComponent<RoomInfo>();

        if (roomInfo.getRoomDepth() + 1 == _dungeonDepth)
        {
            availableRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.GetDoorNumber() == 1;
            }).ToArray();
        }
        else if(roomInfo.getRoomDepth() <= _dungeonDepth / 2)
        {
            availableRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.GetDoorNumber() > 1;
            }).ToArray();
        }

        if (roomInfo.Up && !roomInfo.HasRoomInDirection(RoomInfo.RoomDirection.up))
        {
            var upRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.Down;
            }).ToArray();

            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(0, 20)))
            {
                upRooms = upRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Up;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(-18, 10)))
            {
                upRooms = upRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Left;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(18, 10)))
            {
                upRooms = upRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Right;
                }).ToArray();
            }

            var upRoom = Instantiate(upRooms[Random.Range(0, upRooms.Length)], transform);
            var upRoomInfo = upRoom.GetComponent<RoomInfo>();
            upRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            upRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(0, 10));
            upRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.down, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.up, upRoom);

            _roomList.Add(upRoom);

            GenerateRooms();
        }
        if (roomInfo.Down && !roomInfo.HasRoomInDirection(RoomInfo.RoomDirection.down))
        {
            var downRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.Up;
            }).ToArray();

            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(0, -20)))
            {
                downRooms = downRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Down;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(-18, -10)))
            {
                downRooms = downRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Left;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(18, -10)))
            {
                downRooms = downRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Right;
                }).ToArray();
            }

            var downRoom = Instantiate(downRooms[Random.Range(0, downRooms.Length)], transform);
            var downRoomInfo = downRoom.GetComponent<RoomInfo>();
            downRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            downRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(0, -10));
            downRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.up, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.down, downRoom);

            _roomList.Add(downRoom);

            GenerateRooms();
        }
        if (roomInfo.Left && !roomInfo.HasRoomInDirection(RoomInfo.RoomDirection.left))
        {
            var leftRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.Right;
            }).ToArray();

            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(-18 * 2, 0)))
            {
                leftRooms = leftRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Left;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(-18, -10)))
            {
                leftRooms = leftRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Down;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(-18, 10)))
            {
                leftRooms = leftRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Up;
                }).ToArray();
            }

            var leftRoom = Instantiate(leftRooms[Random.Range(0, leftRooms.Length)], transform);
            var leftRoomInfo = leftRoom.GetComponent<RoomInfo>();
            leftRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            leftRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(-18, 0));
            leftRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.right, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.left, leftRoom);

            _roomList.Add(leftRoom);

            GenerateRooms();
        }
        if (roomInfo.Right && !roomInfo.HasRoomInDirection(RoomInfo.RoomDirection.right))
        {
            var rightRooms = availableRooms.Where(room =>
            {
                RoomInfo info = room.GetComponent<RoomInfo>();
                return info.Left;
            }).ToArray();

            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(18 * 2, 0)))
            {
                rightRooms = rightRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Right;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(18, -10)))
            {
                rightRooms = rightRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Down;
                }).ToArray();
            }
            if (CheckIfRoomExists(roomInfo.GetRoomPosition() + new Vector2(18, 10)))
            {
                rightRooms = rightRooms.Where(room =>
                {
                    RoomInfo info = room.GetComponent<RoomInfo>();
                    return !info.Up;
                }).ToArray();
            }

            var rightRoom = Instantiate(rightRooms[Random.Range(0, rightRooms.Length)], transform);
            var leftRoomInfo = rightRoom.GetComponent<RoomInfo>();
            leftRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            leftRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(18, 0));
            leftRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.left, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.right, rightRoom);

            _roomList.Add(rightRoom);

            GenerateRooms();
        }
    }

    bool CheckIfRoomExists(Vector2 position)
    {
        return _roomList.Any(room => room.GetComponent<RoomInfo>().GetRoomPosition() == position);
    }
}
