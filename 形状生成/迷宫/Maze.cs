// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.Maze
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*迷宫*/
/*namespace CreatorModAPI-=  public class Maze*/
using Game;
using System.Collections.Generic;

namespace CreatorModAPI
{
    public class Maze
    {
        private Maze.Room[,] roomMatrix;
        private List<List<Maze.Room>> roads;
        private readonly Random random;

        public Maze(int width, int height)
        {
            random = new Random();
            InstRooms(width, height);
            OrganizeRooms();
            SetFixedDoor();
            Interlink();
        }

        private void InstRooms(int width, int height)
        {
            roomMatrix = new Maze.Room[width, height];
            roads = new List<List<Maze.Room>>();
            for (int index1 = 0; index1 < width; ++index1)
            {
                for (int index2 = 0; index2 < height; ++index2)
                {
                    List<Maze.Room> roomList = new List<Maze.Room>();
                    roomMatrix[index1, index2] = new Maze.Room();
                    roomList.Add(roomMatrix[index1, index2]);
                    roads.Add(roomList);
                }
            }
        }

        private void OrganizeRooms()
        {
            for (int index1 = 0; index1 < roomMatrix.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < roomMatrix.GetLength(1) - 1; ++index2)
                {
                    roomMatrix[index1, index2].BottonDoor = roomMatrix[index1, index2 + 1].TopDoor;
                }
            }
            for (int index1 = 0; index1 < roomMatrix.GetLength(1); ++index1)
            {
                for (int index2 = 0; index2 < roomMatrix.GetLength(0) - 1; ++index2)
                {
                    roomMatrix[index2, index1].RightDoor = roomMatrix[index2 + 1, index1].LeftDoor;
                }
            }
        }

        private void SetFixedDoor()
        {
            for (int index = 0; index < roomMatrix.GetLength(0); ++index)
            {
                roomMatrix[index, 0].TopDoor.IsFixed = true;
            }

            for (int index = 0; index < roomMatrix.GetLength(0); ++index)
            {
                roomMatrix[index, roomMatrix.GetLength(1) - 1].BottonDoor.IsFixed = true;
            }

            for (int index = 0; index < roomMatrix.GetLength(1); ++index)
            {
                roomMatrix[0, index].LeftDoor.IsFixed = true;
            }

            for (int index = 0; index < roomMatrix.GetLength(1); ++index)
            {
                roomMatrix[roomMatrix.GetLength(0) - 1, index].RightDoor.IsFixed = true;
            }
        }

        private void Interlink()
        {
            while (!AllRoomLinked())
            {
                List<Maze.Door> outlineDoors = GetOutlineDoors(roads[random.Int(0, 1048575) % roads.Count]);
                Maze.Door door = outlineDoors[random.Int(0, 1048575) % outlineDoors.Count];
                List<List<Maze.Room>> oldRoads = GetOldRoads(door);
                List<Maze.Room> newRoad = GetNewRoad(oldRoads);
                RemoveOldRoads(oldRoads);
                roads.Add(newRoad);
                door.OpenTheDoor();
            }
        }

        private void RemoveOldRoads(List<List<Maze.Room>> oldRoads)
        {
            foreach (List<Maze.Room> oldRoad in oldRoads)
            {
                roads.Remove(oldRoad);
            }
        }

        private List<Maze.Room> GetNewRoad(List<List<Maze.Room>> oldRoad)
        {
            List<Maze.Room> roomList1 = new List<Maze.Room>();
            foreach (List<Maze.Room> roomList2 in oldRoad)
            {
                foreach (Maze.Room room in roomList2)
                {
                    roomList1.Add(room);
                }
            }
            return roomList1;
        }

        private List<List<Maze.Room>> GetOldRoads(Maze.Door door)
        {
            List<List<Maze.Room>> roomListList = new List<List<Maze.Room>>();
            foreach (List<Maze.Room> road in roads)
            {
                foreach (Maze.Room room in road)
                {
                    if (TwoDoorAreEqual(room.TopDoor, door))
                    {
                        roomListList.Add(road);
                        break;
                    }
                    if (TwoDoorAreEqual(room.BottonDoor, door))
                    {
                        roomListList.Add(road);
                        break;
                    }
                    if (TwoDoorAreEqual(room.LeftDoor, door))
                    {
                        roomListList.Add(road);
                        break;
                    }
                    if (TwoDoorAreEqual(room.RightDoor, door))
                    {
                        roomListList.Add(road);
                        break;
                    }
                }
            }
            return roomListList;
        }

        private bool TwoDoorAreEqual(Maze.Door doorSource, Maze.Door doorTarget) => doorSource.GetLockState() && !doorSource.IsFixed && object.Equals(doorSource, doorTarget);

        private bool AllRoomLinked() => roads.Count == 1;

        private List<Maze.Door> GetOutlineDoors(List<Maze.Room> road)
        {
            List<Maze.Door> outlineDoors = new List<Maze.Door>();
            foreach (Maze.Room room in road)
            {
                AddOutlineDoor(room.TopDoor, outlineDoors);
                AddOutlineDoor(room.BottonDoor, outlineDoors);
                AddOutlineDoor(room.LeftDoor, outlineDoors);
                AddOutlineDoor(room.RightDoor, outlineDoors);
            }
            return outlineDoors;
        }

        private void AddOutlineDoor(Maze.Door door, List<Maze.Door> outlineDoors)
        {
            if (!door.GetLockState() || door.IsFixed)
            {
                return;
            }

            if (!outlineDoors.Contains(door))
            {
                outlineDoors.Add(door);
            }
            else
            {
                outlineDoors.Remove(door);
            }
        }

        private bool[,] RoomToData()
        {
            bool[,] dataMatrix = new bool[roomMatrix.GetLength(0) * 2 + 1, roomMatrix.GetLength(1) * 2 + 1];
            PreFill(dataMatrix);
            for (int xPos = 0; xPos < roomMatrix.GetLength(0); ++xPos)
            {
                for (int yPos = 0; yPos < roomMatrix.GetLength(1); ++yPos)
                {
                    SetData(dataMatrix, xPos, yPos, -1, 0, roomMatrix[xPos, yPos].LeftDoor.GetLockState());
                    SetData(dataMatrix, xPos, yPos, 1, 0, roomMatrix[xPos, yPos].RightDoor.GetLockState());
                    SetData(dataMatrix, xPos, yPos, 0, -1, roomMatrix[xPos, yPos].TopDoor.GetLockState());
                    SetData(dataMatrix, xPos, yPos, 0, 1, roomMatrix[xPos, yPos].BottonDoor.GetLockState());
                }
            }
            return dataMatrix;
        }

        private void SetData(
          bool[,] dataMatrix,
          int xPos,
          int yPos,
          int xOffset,
          int yOffset,
          bool isClose)
        {
            dataMatrix[xPos * 2 + 1 + xOffset, yPos * 2 + 1 + yOffset] = isClose;
        }

        private void PreFill(bool[,] dataMatrix)
        {
            for (int index1 = 0; index1 < dataMatrix.GetLength(0); index1 += 2)
            {
                for (int index2 = 0; index2 < dataMatrix.GetLength(1); index2 += 2)
                {
                    dataMatrix[index1, index2] = true;
                }
            }
        }

        public bool[,] GetBoolArray() => RoomToData();

        private class Room
        {
            private Maze.Door topDoor;
            private Maze.Door bottonDoor;
            private Maze.Door leftDoor;
            private Maze.Door rightDoor;
            private Maze.Door topLeftDoor;
            private Maze.Door topRightDoor;
            private Maze.Door bottonLeftDoor;
            private Maze.Door bottonRightDoor;

            public Maze.Door TopDoor
            {
                get => topDoor;
                set => topDoor = value;
            }

            public Maze.Door BottonDoor
            {
                get => bottonDoor;
                set => bottonDoor = value;
            }

            public Maze.Door LeftDoor
            {
                get => leftDoor;
                set => leftDoor = value;
            }

            public Maze.Door RightDoor
            {
                get => rightDoor;
                set => rightDoor = value;
            }

            public Maze.Door TopLeftDoor
            {
                get => topLeftDoor;
                set => topLeftDoor = value;
            }

            public Maze.Door TopRightDoor
            {
                get => topRightDoor;
                set => topRightDoor = value;
            }

            public Maze.Door BottonLeftDoor
            {
                get => bottonLeftDoor;
                set => bottonLeftDoor = value;
            }

            public Maze.Door BottonRightDoor
            {
                get => bottonRightDoor;
                set => bottonRightDoor = value;
            }

            public Room()
            {
                topDoor = new Maze.Door();
                bottonDoor = new Maze.Door();
                leftDoor = new Maze.Door();
                rightDoor = new Maze.Door();
                topLeftDoor = new Maze.Door();
                topRightDoor = new Maze.Door();
                bottonLeftDoor = new Maze.Door();
                bottonRightDoor = new Maze.Door();
            }
        }

        private class Door
        {
            private bool isLocked;
            private bool isFixed;

            public bool IsFixed
            {
                get => isFixed;
                set => isFixed = value;
            }

            public Door() => isLocked = true;

            public void OpenTheDoor() => isLocked = false;

            public void CloseTheDoor() => isLocked = true;

            public bool GetLockState() => isLocked;
        }
    }
}
