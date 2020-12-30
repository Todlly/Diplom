using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class Room
    {
        GameObject RoomObject;
        List<GameObject> Enemies { get; set; }
        [SerializeField]
        List<Door> Doors;
        public Vector3 Coordinates;

        public void LoadRoom()
        {
            RoomObject.SetActive(true);
            foreach (Door door in Doors)
            {
                door.DoorObect.SetActive(true);
            }
        }

        public void HideRoom()
        {
            RoomObject.SetActive(false);
            foreach (Door door in Doors)
            {
                door.DoorObect.SetActive(false);
            }
        }

        public Room(Vector3 coordinates, GameObject doorPrefab, Door firstDoor)
        {
            RoomObject = GameObject.Instantiate(doorPrefab, Vector3.zero, Quaternion.identity);
            RoomObject.transform.position = coordinates;

            Doors = new List<Door>();

            Doors[0] = firstDoor;

            for(int i = 0; i < 4; i++)
            {

            }

            foreach (Door door in Doors)
            {
                Transform doorPlace = null;
                switch (door.Side)
                {
                    case DoorSide.North:
                        doorPlace = RoomObject.transform.Find("NorthDoorway");
                        break;
                    case DoorSide.South:
                        doorPlace = RoomObject.transform.Find("SouthDoorway");
                        break;
                    case DoorSide.West:
                        doorPlace = RoomObject.transform.Find("WestDoorway");
                        break;
                    case DoorSide.East:
                        doorPlace = RoomObject.transform.Find("EastDoorway");
                        break;
                }
                GameObject.Instantiate(door.DoorObect, doorPlace);
            }

          //  HideRoom();
        }
    }
}
