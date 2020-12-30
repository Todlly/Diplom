using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum DoorSide { North, South, West, East }

    public class Door
    {
        public DoorSide Side;
        public UnityEngine.Object Prefab = Resources.Load("Assets / Prefabs / Environment / Door.prefab");
        public GameObject DoorObect;
        public int[] RoomsIDs = new int[2];

        public Door(int firstRoomID, DoorSide side)
        {
            RoomsIDs[0] = firstRoomID;
            Side = side;
        }
    }
}
