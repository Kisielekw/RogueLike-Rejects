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
    private GameObject _entryPrefab;

    private List<GameObject> _roomList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool conntinueGeneration = true;
        do
        {
            _roomList = new List<GameObject> { Instantiate(_entryPrefab, transform) };
            var roomInfo = _roomList[0].GetComponent<RoomInfo>();
            roomInfo.SetRoomDepth(0);
            roomInfo.SetRoomPosition(Vector2.zero);

            GenerateRooms();

            conntinueGeneration = false;
            for (int i = 0; i < _roomList.Count; i++)
            {
                for(int j = 0; j < _roomList.Count; j++)
                {
                    if (i == j)
                        continue;
                    var room1 = _roomList[i].GetComponent<RoomInfo>();
                    var room2 = _roomList[j].GetComponent<RoomInfo>();
                    if (room1.GetRoomPosition() == room2.GetRoomPosition())
                    {
                        conntinueGeneration = true;
                        break;
                    }
                }
                if (conntinueGeneration)
                    break;
            }

            if(conntinueGeneration)
            {
                foreach (var room in _roomList)
                {
                    Destroy(room);
                }
            }

            _roomList.Clear();

        } while (conntinueGeneration);
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

            var upRoom = Instantiate(upRooms[Random.Range(0, upRooms.Length)], transform);
            var upRoomInfo = upRoom.GetComponent<RoomInfo>();
            upRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            upRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(0, 10));
            upRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.down, room);

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

            var downRoom = Instantiate(downRooms[Random.Range(0, downRooms.Length)], transform);
            var downRoomInfo = downRoom.GetComponent<RoomInfo>();
            downRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            downRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(0, -10));
            downRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.up, room);

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

            var leftRoom = Instantiate(leftRooms[Random.Range(0, leftRooms.Length)], transform);
            var leftRoomInfo = leftRoom.GetComponent<RoomInfo>();
            leftRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            leftRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(-18, 0));
            leftRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.right, room);

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

            var rightRoom = Instantiate(rightRooms[Random.Range(0, rightRooms.Length)], transform);
            var leftRoomInfo = rightRoom.GetComponent<RoomInfo>();
            leftRoomInfo.SetRoomDepth(roomInfo.getRoomDepth() + 1);
            leftRoomInfo.SetRoomPosition(roomInfo.GetRoomPosition() + new Vector2(18, 0));
            leftRoomInfo.SetRoomDirection(RoomInfo.RoomDirection.left, room);

            _roomList.Add(rightRoom);

            GenerateRooms();
        }
    }

    bool CheckIfRoomExists(Vector2 position)
    {
        return _roomList.Any(room => room.GetComponent<RoomInfo>().GetRoomPosition() == position);
    }
}
