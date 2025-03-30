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
    private GameObject[] _entryPrefabs;

    [SerializeField]
    private GameObject[] _bossRoomPrefabs;

    private List<GameObject> _roomList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _roomList = new List<GameObject> { Instantiate(_entryPrefabs[Random.Range(0,_entryPrefabs.Length)], transform) };
        var roomInfo = _roomList[0].GetComponent<RoomInfo>();
        roomInfo.SetRoomDepth(0);
        roomInfo.SetRoomPosition(Vector2.zero);

        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.SetCurrentRoom(_roomList[0]);

        GenerateRooms();

        var oneDoorRooms = _roomList.Where(room =>
        {
            var roomInfo = room.GetComponent<RoomInfo>();
            return roomInfo.GetDoorNumber() == 1 && roomInfo.GetRoomPosition() != Vector2.zero;
        }).ToList();

        
        GenerateBossRoom(oneDoorRooms);
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

    void GenerateBossRoom(List<GameObject> oneDoorRooms)
    {
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        var furthestRoom = oneDoorRooms.Aggregate((room1, room2) =>
        {
            var room1Info = room1.GetComponent<RoomInfo>();
            var room2Info = room2.GetComponent<RoomInfo>();
            var room1Distance = Vector2.Distance(player.transform.position, room1Info.GetRoomPosition());
            var room2Distance = Vector2.Distance(player.transform.position, room2Info.GetRoomPosition());
            return room1Distance > room2Distance ? room1 : room2;
        });

        oneDoorRooms.Remove(furthestRoom);

        RoomInfo furthestInfo = furthestRoom.GetComponent<RoomInfo>();
        Vector2 furthestPosition = furthestInfo.GetRoomPosition();

        if (furthestInfo.Up)
        {
            var room = furthestInfo.GetRoomInDirection(RoomInfo.RoomDirection.up);
            var roomInfo = room.GetComponent<RoomInfo>();
            _roomList.Remove(furthestRoom);
            Destroy(furthestRoom);
            var newRoom = Instantiate(_bossRoomPrefabs.First(room =>
            {
                var roomInfo = room.GetComponent<RoomInfo>();
                return roomInfo.Up;
            }), transform);
            var newRoomInfo = newRoom.GetComponent<RoomInfo>();
            newRoomInfo.SetRoomDepth(furthestInfo.getRoomDepth());
            newRoomInfo.SetRoomPosition(furthestPosition);
            newRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.up, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.down, newRoom);

        }
        else if (furthestInfo.Down)
        {
            var room = furthestInfo.GetRoomInDirection(RoomInfo.RoomDirection.down);
            var roomInfo = room.GetComponent<RoomInfo>();
            _roomList.Remove(furthestRoom);
            Destroy(furthestRoom);
            var newRoom = Instantiate(_bossRoomPrefabs.First(room =>
            {
                var roomInfo = room.GetComponent<RoomInfo>();
                return roomInfo.Down;
            }), transform);
            var newRoomInfo = newRoom.GetComponent<RoomInfo>();
            newRoomInfo.SetRoomDepth(furthestInfo.getRoomDepth());
            newRoomInfo.SetRoomPosition(furthestPosition);
            newRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.down, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.up, newRoom);
        }
        else if (furthestInfo.Left)
        {
            var room = furthestInfo.GetRoomInDirection(RoomInfo.RoomDirection.left);
            var roomInfo = room.GetComponent<RoomInfo>();
            _roomList.Remove(furthestRoom);
            Destroy(furthestRoom);
            var newRoom = Instantiate(_bossRoomPrefabs.First(room =>
            {
                var roomInfo = room.GetComponent<RoomInfo>();
                return roomInfo.Left;
            }), transform);
            var newRoomInfo = newRoom.GetComponent<RoomInfo>();
            newRoomInfo.SetRoomDepth(furthestInfo.getRoomDepth());
            newRoomInfo.SetRoomPosition(furthestPosition);
            newRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.left, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.right, newRoom);
        }
        else if (furthestInfo.Right)
        {
            var room = furthestInfo.GetRoomInDirection(RoomInfo.RoomDirection.right);
            var roomInfo = room.GetComponent<RoomInfo>();
            _roomList.Remove(furthestRoom);
            Destroy(furthestRoom);
            var newRoom = Instantiate(_bossRoomPrefabs.First(room =>
            {
                var roomInfo = room.GetComponent<RoomInfo>();
                return roomInfo.Right;
            }), transform);
            var newRoomInfo = newRoom.GetComponent<RoomInfo>();
            newRoomInfo.SetRoomDepth(furthestInfo.getRoomDepth());
            newRoomInfo.SetRoomPosition(furthestPosition);
            newRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.right, room);
            roomInfo.SetRoomDirection(RoomInfo.RoomDirection.left, newRoom);
        }
    }
}
