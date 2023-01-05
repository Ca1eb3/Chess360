// Caleb Smith
// 01/04/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public static class MovePatterns
    {
        public static bool DiagonalMove(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            bool[] IsValidMove = new bool[4] { true, true, true, true };
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<TileBehaviour>();
            TileBehaviour temp = tempObject.GetComponent<TileBehaviour>();
            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[0] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[0] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[1] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[1] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[2] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[2] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[3] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[3] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            if (IsValidMove[0] || IsValidMove[1] || IsValidMove[2] || IsValidMove[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DiagonalMovePilot(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            bool[] IsValidMove = new bool[4] { true, true, true, true };
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<TileBehaviour>();
            TileBehaviour temp = tempObject.GetComponent<TileBehaviour>();
            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[0] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[0] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[1] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[1] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[2] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[2] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[3] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[3] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            if (IsValidMove[0] || IsValidMove[1] || IsValidMove[2] || IsValidMove[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}