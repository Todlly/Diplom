using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsKeeping : MonoBehaviour
{
    public List<Room> Rooms;
    public GameObject RoomPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Rooms = new List<Room>();
        Rooms.Add(new Room(Vector3.zero, RoomPrefab, new Door(0, DoorSide.North)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            foreach(Room room in Rooms)
            {
                room.LoadRoom();
            }
        }else if (Input.GetKey(KeyCode.DownArrow))
        {
            foreach(Room room in Rooms)
            {
                room.HideRoom();
            }
        }
    }


}